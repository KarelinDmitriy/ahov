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
		private readonly IBarrelProviderFactory _barrelProviderFactory;
		private readonly IMatterProvider _matterProvider;
		private readonly IOrgDataproviderFactory orgProviderFactory;

		public BarrelController(IBarrelProviderFactory barrelProviderFactory,
			IMatterProvider matterProvider,
			IOrgDataproviderFactory orgProviderFactory)
		{
			_barrelProviderFactory = barrelProviderFactory;
			_matterProvider = matterProvider;
			this.orgProviderFactory = orgProviderFactory;
		}

		public ActionResult List(int orgId)
		{
			var barrels = _barrelProviderFactory
				.CreateBarrelProvider(0)
				.GetBarrels(orgId);
			var availableMatter = _matterProvider.GetAllDatas();

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
				bar = _barrelProviderFactory.CreateBarrelProvider(0).AddBarrel(barrel);
			}
			catch (Exception ec)
			{
				
				throw;
			}
			//todo: проверить, что пользователь имеет доступ
			var model = new BarrelModel
			{
				Barrel = bar,
				AvailableMatter = _matterProvider.GetAllDatas()
			};
			return PartialView("Barrel", model);
		}

		[HttpPost]
		public ActionResult Edit(BarrelModel model)
		{
			return null;
		}
	}
}