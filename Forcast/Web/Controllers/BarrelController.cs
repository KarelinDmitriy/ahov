using System;
using System.Linq;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using AhovRepository.Factory;
using AhovRepository.Repository;
using Web.Core;
using Web.Models.Barrel;

namespace Web.Controllers
{
	[AppAuthorize]
	public class BarrelController : Controller
	{
		private readonly IBarrelProviderFactory barrelProviderFactory;
		private readonly IMatterProvider matterProvider;
		private readonly IOrgDataproviderFactory orgProviderFactory;
		private readonly IAccessProvider accessProvider;
		private readonly IDatabaseProvider databaseProvider;

		public BarrelController(IBarrelProviderFactory barrelProviderFactory,
			IMatterProvider matterProvider,
			IOrgDataproviderFactory orgProviderFactory,
			IAccessProvider accessProvider,
			IDatabaseProvider databaseProvider)
		{
			this.barrelProviderFactory = barrelProviderFactory;
			this.matterProvider = matterProvider;
			this.orgProviderFactory = orgProviderFactory;
			this.accessProvider = accessProvider;
			this.databaseProvider = databaseProvider;
		}

		public ActionResult List(int orgId)
		{
			var cityObjectId = orgProviderFactory
				.CreateOrgProvider(HttpContext.GetUserId())
				.GetOrg(orgId)?.ObjectId;
			var accessType = accessProvider.GetAccessType(HttpContext.GetUserId(), cityObjectId);
			if (accessType == AccessType.None)
				throw new AccessDeniedExpection("Организация не существует или недостаточно прав");
			var barrels = barrelProviderFactory
				.CreateBarrelProvider(0)
				.GetBarrels(orgId);
			var availableMatter = matterProvider.GetAllDatas();

			var model = new BarrelsModel
			{
				Barrels = barrels.Select(x => new BarrelModel {Barrel = x, AvailableMatter = availableMatter }).ToList(),
				OrgId = orgId,
			};
			return View(model);
		}

		public ActionResult AddNew(int orgId)
		{
			BarrelEntity bar;
			var org = orgProviderFactory.CreateOrgProvider(HttpContext.GetUserId()).GetOrg(orgId); //TODO: Защита от хаков
			try
			{
				var barrel = new BarrelEntity
				{
					BarrelId = 0,
					Org = new OrgEntity { Id = orgId },
					Name = "Без имени",
					Code = "0",
					Draining = "О",
					H = 0,
					Q = 0,
					SaveType = "Д",
					ObjectId = org.ObjectId
				};
				bar = barrelProviderFactory.CreateBarrelProvider(0).AddBarrel(barrel);
			}
			catch (Exception ec)
			{
				
				throw;
			}
			//todo: проверить, что пользователь имеет доступ
			var model = new BarrelModel
			{
				Barrel = bar,
				AvailableMatter = matterProvider.GetAllDatas()
			};
			return PartialView("Barrel", model);
		}

		[HttpPost]
		public ActionResult Edit(BarrelModel model)
		{
			var success = true;
			try
			{
				barrelProviderFactory.CreateBarrelProvider(HttpContext.GetUserId()).UpdateBarrel(model.Barrel);
			}
			catch (Exception)
			{
				success = false;
			}
			return PartialView("OperationResult", success);
		}

		public ActionResult Delete(int barrelId)
		{
			try
			{
				databaseProvider.Delete(new BarrelEntity { BarrelId = barrelId });
			}
			catch (Exception)
			{
				return new JsonResult {Data = new { success = false }, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
			}
			return new JsonResult {Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
		}
	}
}