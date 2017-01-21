﻿using System;
using System.Web.Mvc;
using AhovRepository.Entity;
using AhovRepository.Factory;

namespace Web.Controllers
{
	public class CityController : Controller
	{
		private readonly ICityProviderFactory _providerFactory;

		public CityController(ICityProviderFactory providerFactory)
		{
			_providerFactory = providerFactory;
		}

		public ActionResult List()
		{
			var provider = _providerFactory.CreateCityProvider(0);
			var city = provider.GetCities();
			return View(city);
		}

		public ActionResult Create()
		{
			var city = new CityEntity();
			return View(city);
		}

		[HttpPost]
		public ActionResult Create(CityEntity model)
		{
			var provider = _providerFactory.CreateCityProvider(0);
			provider.AddCity(model);
			return RedirectToAction("List");
		}

		public ActionResult Edit(int cityId)
		{
			var provider = _providerFactory.CreateCityProvider(0);
			var city = provider.GetCity(cityId);
			return View(city);
		}

		[HttpPost]
		public ActionResult Edit(CityEntity model)
		{
			var provider = _providerFactory.CreateCityProvider(0);
			provider.UpdateCity(model);
			return RedirectToAction("List");
		}

		public ActionResult NewCityType(int cityId)
		{
			return PartialView("CityTypesTab", new CityTypeEntity {City = new CityEntity {CityId = cityId}});
		}

		[HttpPost]
		public PartialViewResult EditCityType(CityTypeEntity cityType)
		{
			var success = true;
			var provider = _providerFactory.CreateCityProvider(0);
			try
			{
				provider.UpdateCityType(cityType);
			}
			catch (Exception ex)
			{
				success = false;
			}
			return PartialView("CityTypeSaveResult", success);
		}

		[HttpPost]
		public JsonResult AddNewCityType(CityTypeEntity cityType)
		{
			var provider = _providerFactory.CreateCityProvider(0);
			provider.AddCityType(cityType);
			return new JsonResult {Data = new {success = true}};
		}
	}
}