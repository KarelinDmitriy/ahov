using System;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Factory;
using AhovRepository.Repository;
using Forcast;
using Web.Core;
using Web.Models.Forcast;
using System.Collections.Generic;
using System.Linq;
using AhovRepository.Entity;
using Forcast.Matters;
using Forcast.V2;

namespace Web.Controllers
{
	public class ForcastController : Controller
	{
		private readonly IOrgDataproviderFactory orgFactory;
		private readonly IMatterProvider matterProvider;
		private readonly ICityProvider cityProvider;
		private readonly IDatabaseProvider databaseProvider;

		public ForcastController(IOrgDataproviderFactory orgFactory,
			IMatterProvider matterProvider,
			ICityProvider cityProvider,
			IDatabaseProvider databaseProvider)
		{
			this.orgFactory = orgFactory;
			this.matterProvider = matterProvider;
			this.cityProvider = cityProvider;
			this.databaseProvider = databaseProvider;
		}

		public ActionResult Edit(int orgId)
		{
			var org = orgFactory.CreateOrgProvider(HttpContext.GetUserId());
			if (org == null)
				throw new AccessDeniedExpection("Нет доступа к организации");
			var model = new ActiveModel
			{
				OrgId = orgId
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Result(ActiveModel model)
		{
			var activeData = FillActiveData(model);
			var storageData = FillStorageData(model.OrgId);
			var barrels = GetBarrels(model.OrgId);
			var forcaster = new Forcaster(barrels, storageData, activeData);
			forcaster.Run();
			var fModel = new ResultModel
			{
				Result = forcaster.Result,
				ForDef = forcaster.CalculatorWithDef.iv,
				WithoutDef = forcaster.CalculatorWithoutDef.iv
			};
			return View(fModel);
		}

		private static ActiveData FillActiveData(ActiveModel activeModel)
		{
			var activeData = new ActiveData
			{
				U = activeModel.WildSpeed,
				T = activeModel.T,
				To = activeModel.To,
				AirVerticalStable = new Table_3_3(activeModel.StateType),
				Tcw = activeModel.Temperature,
				Tn = new[] {activeModel.Tn, activeModel.Tn},
				q = activeModel.Q,
				Ku2 = 1,
				Ku9 = 1
			};
			return activeData;
		}

		private StorageData FillStorageData(int orgId)
		{
			var org = orgFactory.CreateOrgProvider(HttpContext.GetUserId()).GetOrg(orgId);
			var city = cityProvider.GetCity(org.City.CityId);
			return  new StorageData
			{
				Цх = city.Lenght,
				Цу = city.Width,
				Цx_p = org.Length,
				Цу_p = org.Width,
				Ro = org.Ro,
				No = org.PersonalCount,
				Gdl = org.Gdl,
				Gl = org.Gl,
				Kf = org.Kf,
				N = city.Population,
				Rz = org.Rz,
				Top = org.Top,
				Tow = org.Tow,
				W = org.W,
				b = new[] {0, 0d},
				a = new[] {(1 - (double)city.ChildPercent / 100), (double)city.ChildPercent/100},
				aao = org.Aao,
				aa = new[] { city.Aa, city.AaChild},
				apr = new[] {city.Apr, city.AprChild},
				au = new[] {city.Au, city.AuChild},
				aw = new[] {city.Aw, city.AwChild},
				ba = new[] {org.Ba, org.Ba_ch},
				bu = new[] {org.Bu, org.Bu_ch},
				bw = new[] {org.Bw, org.Bw_ch},
				QInside = new[] {new QInside { Ay = 1, Kp = 1} }
			};
		}

		private List<BarrelV2> GetBarrels(int orgId)
		{
			var bars = databaseProvider.Where<BarrelEntity>(x => x.Org.Id == orgId);
			return bars.Select(x => new BarrelV2
			{
				Matter = new Matter
				{
					Data = matterProvider.GetDataByCode(x.Code),
				},
				Draining = ToDraining(x.Draining),
				Q = x.Q,
				H = x.H,
				SaveType = ToMatterSaveType(x.SaveType)
			}).ToList();
		}

		private Draining ToDraining(string value)
		{
			switch (value)
			{
				case "В поддон":
					return Draining.Vp1;
				case "На ровную поверхность":
					return Draining.Vp2;
			}
			throw new ArgumentException();
		}

		private MatterSaveType ToMatterSaveType(string value)
		{
			switch (value)
			{
				case "Изотермический":
					return MatterSaveType.Cx1;
				case "Под давлением":
					return MatterSaveType.Cx2;
				case "Обычный":
					return MatterSaveType.Cx3;
			}
			throw new ArgumentException();
		}
	}
}