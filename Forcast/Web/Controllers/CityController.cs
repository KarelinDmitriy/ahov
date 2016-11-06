using System;
using System.Linq;
using System.Web.Mvc;
using AhovRepository;
using Web.Models.City;

namespace Web.Controllers
{
	public class CityController : Controller
	{
		private readonly IAhovRepository _repository;
		public CityController(IAhovRepository repository)
		{
			_repository = repository;
		}

		public ActionResult List()
		{
			var city = _repository.GetAvalibleCity(1);
			return View(city);
		}

		public ActionResult Create()
		{
			var city = new WebCity();
			return View(city);
		}

		[HttpPost]
		public ActionResult Create(WebCity model)
		{
			_repository.AddNewCity(model);
			return RedirectToAction("List");
		}

		public ActionResult Edit(int cityId)
		{
			var city = _repository.GetCity(cityId, 1);
			return View(city);
		}

		[HttpPost]
		public ActionResult Edit(WebCity model)
		{
			_repository.UpdateCity(model, 1);
			return RedirectToAction("List");
		}

		public ActionResult NewCityType(int cityId)
		{
			return PartialView("CityTypesTab", new CityType {Id = cityId});
		}

		[HttpPost]
		public PartialViewResult EditCityType(CityType cityType)
		{
			var success = true;
			try
			{
				_repository.UpdateCityType(cityType, 1);
			}
			catch (Exception ex)
			{
				success = false;
			}
			return PartialView("CityTypeSaveResult", success);
		}

		[HttpPost]
		public JsonResult AddNewCityType(CityType cityType)
		{
			//TODO: не забыть использовать город
			_repository.AddNewCityType(cityType, 1);
			return new JsonResult { Data = new { success = true } };
		}
	}
}