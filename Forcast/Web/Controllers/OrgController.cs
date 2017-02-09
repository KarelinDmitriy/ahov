using System.Web.Mvc;
using AhovRepository.Entity;
using AhovRepository.Factory;
using AhovRepository.Repository;
using Web.Core;
using Web.Models.Org;

namespace Web.Controllers
{
	[AppAuthorize]
	public class OrgController : Controller
	{
		private readonly IOrgDataproviderFactory _orgProviderFactory;
		private readonly ICityProviderFactory _cityProviderFactory;

		public OrgController(IOrgDataproviderFactory orgProviderFactory, ICityProviderFactory cityProviderFactory)
		{
			_orgProviderFactory = orgProviderFactory;
			_cityProviderFactory = cityProviderFactory;
		}

		public ActionResult List()
		{
			var orgProvider = GetProvider();
			var orgs = orgProvider.GetOrgs();
			return View(orgs);
		}

		private IOrgProvider GetProvider()
		{
			return _orgProviderFactory.CreateOrgProvider(HttpContext.GetUserId());
		}

		public ActionResult Create()
		{
			var org = new OrgEntity()
			{City = new CityEntity()};
			var cities = _cityProviderFactory.CreateCityProvider(0).GetCities();
			var model = new OrgModel
			{
				Org = org,
				AvaliableCities = cities
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(OrgModel model)
		{
			GetProvider().AddOrganization(model.Org);
			return RedirectToAction("List");
		}

		public ActionResult Edit(int orgId)
		{
			var org = GetProvider().GetOrg(orgId);
			var avaliableCities = _cityProviderFactory.CreateCityProvider(0).GetCities();
			var model = new OrgModel
			{
				Org = org,
				AvaliableCities = avaliableCities
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(OrgEntity org)
		{
			var orgProvider = GetProvider();
			var orgEntity = orgProvider.GetOrg(org.Id);
			org.ObjectId = orgEntity.ObjectId;
			orgProvider.UpdateOrganization(org);
			return RedirectToAction("List");
		}
	}
}