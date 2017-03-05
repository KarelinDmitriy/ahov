using System;
using System.Web.Mvc;
using AhovRepository.Entity;
using AhovRepository.Repository;
using Web.Core;
using Web.Models.City;

namespace Web.Controllers
{
	[AppAuthorize]
	public class CityController : Controller
	{
		private readonly ICityProvider cityProvider;

		public CityController(ICityProvider cityProvider)
		{
			this.cityProvider = cityProvider;
		}

		public ActionResult List()
		{
			var city = cityProvider.GetCities();
			var model = new ListCitiesModel
			{
				Cities = city,
				CanCreateCity = HttpContext.GetUser().Role != AppRoles.Client
			};
			return View(model);
		}

		public ActionResult Create()
		{
			var user = HttpContext.GetUser();
			if (user.Role == AppRoles.Client)
				throw new AccessDeniedExpection("Страница недоступна для вас");
			var city = new CityModel();
			return View(city);
		}

		[HttpPost]
		public ActionResult Create(CityModel model)
		{
			var user = HttpContext.GetUser();
			if (user.Role == AppRoles.Client)
				throw new AccessDeniedExpection("Недостаточно прав для регистрации города");
			cityProvider.AddCity(model.City);
			return RedirectToAction("List");
		}

		public ActionResult Show(int cityId)
		{
			var user = HttpContext.GetUser();
			var city = cityProvider.GetCity(cityId);
			var building = cityProvider.GetBuilding(cityId);
			var cityModel = new CityModel
			{
				City = city,
				Buildings = building
			};
			return user.Role == AppRoles.Client ? View("Show", cityModel) : View("Edit", cityModel);
		}

		[HttpPost]
		public ActionResult Edit(CityModel model)
		{
			var user = HttpContext.GetUser();
			if (user.Role == AppRoles.Client)
				throw new AccessDeniedExpection("Недостаточно прав для редактирования данные о городе");
			cityProvider.UpdateCity(model.City);
			return RedirectToAction("List");
		}

		public ActionResult NewCityType(int cityId)
		{
			var user = HttpContext.GetUser();
			if (user.Role == AppRoles.Client)
				throw new AccessDeniedExpection("Недостаточно прав для доступа к странице");
			var cityType = new CityTypeEntity
			{
				Ay = 0,
				Kp = 0,
				Name = "нет имени",
				City = new CityEntity
				{
					CityId = cityId
				}
			};
			var entity = cityProvider.AddCityType(cityType);
			return PartialView("CityTypesTab", entity);
		}

		[HttpPost]
		public PartialViewResult EditCityType(CityTypeEntity cityType)
		{
			var user = HttpContext.GetUser();
			if (user.Role == AppRoles.Client)
				throw new AccessDeniedExpection("Недостаточно прав для редактирования информации о строениях");
			var success = true;
			try
			{
				cityProvider.UpdateCityType(cityType);
			}
			catch (Exception ex)
			{
				success = false;
			}
			return PartialView("OperationResult", success);
		}

		public JsonResult RemoveCityType(int cityTypeId)
		{
			var user = HttpContext.GetUser();
			if (user.Role == AppRoles.Client)
				throw new AccessDeniedExpection("Недостаточно прав для улаления данных о строениях");
			try
			{
				cityProvider.DeleteCityType(cityTypeId);
			}
			catch(Exception ex)
			{
				return new JsonResult {Data = new {success = false}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
			}
			return new JsonResult {Data = new {success = true}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
		}
	}
}