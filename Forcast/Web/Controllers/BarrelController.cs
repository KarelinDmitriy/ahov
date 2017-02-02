using System;
using System.Linq;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using AhovRepository.Factory;
using Web.Models.Barrel;

namespace Web.Controllers
{
	public class BarrelController : Controller
	{
		private readonly IBarrelProviderFactory _barrelProviderFactory;
		private readonly IMatterProvider _matterProvider;

		public BarrelController(IBarrelProviderFactory barrelProviderFactory,
			IMatterProvider matterProvider)
		{
			_barrelProviderFactory = barrelProviderFactory;
			_matterProvider = matterProvider;
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
					SaveType = "Д"
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