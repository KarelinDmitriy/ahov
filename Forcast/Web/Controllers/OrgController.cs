using System.Web.Mvc;
using AhovRepository.Entity;
using AhovRepository.Factory;
using Web.Models.Org;

namespace Web.Controllers
{
	public class OrgController : Controller
	{
		private readonly IOrgDataproviderFactory _orgProviderFactory;
		private readonly ICityProviderFactory _cityProviderFactory;

		public OrgController(IOrgDataproviderFactory orgProviderFactory, ICityProviderFactory cityProviderFactory)
		{
			this._orgProviderFactory = orgProviderFactory;
			_cityProviderFactory = cityProviderFactory;
		}

		public ActionResult List()
		{
			var orgProvider = _orgProviderFactory.CreateOrgProvider(0);
			var orgs = orgProvider.GetOrgs();

			return View(orgs);
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
			var orgProvider = _orgProviderFactory.CreateOrgProvider(0);
			orgProvider.AddOrganization(model.Org);
			return RedirectToAction("List");
		}

		public ActionResult Edit(int orgId)
		{
			var org = _orgProviderFactory.CreateOrgProvider(0).GetOrg(orgId);
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
			_orgProviderFactory.CreateOrgProvider(0).UpdateOrganization(org);
			return RedirectToAction("List");
		}
	}
}