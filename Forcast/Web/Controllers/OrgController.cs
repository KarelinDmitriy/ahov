using System.Linq;
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
		private readonly IOrgDataproviderFactory orgProviderFactory;
		private readonly ICityProvider cityProvider;
		private readonly IAccessProvider accessProvider;

		public OrgController(IOrgDataproviderFactory orgProviderFactory,
			ICityProvider cityProvider,
			IAccessProvider accessProvider)
		{
			this.orgProviderFactory = orgProviderFactory;
			this.cityProvider = cityProvider;
			this.accessProvider = accessProvider;
		}

		public ActionResult List()
		{
			var orgProvider = GetProvider();
			var orgs = orgProvider.GetOrgs();
			var userId = HttpContext.GetUserId();
			var model = new ListOrgModel
			{
				Items = orgs.Select(x => new OrgItem
				{
					Org = x,
					CanChangeAccess = CanChangeAccess(accessProvider.GetAccessType(userId, x.ObjectId))
				}).ToList()
			};
			return View(model);
		}

		private IOrgProvider GetProvider()
		{
			return orgProviderFactory.CreateOrgProvider(HttpContext.GetUserId());
		}

		public ActionResult Create()
		{
			var org = new OrgEntity()
			{City = new CityEntity()};
			var cities = cityProvider.GetCities();
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
			if (org == null)
				throw new AccessDeniedExpection("Организация не существует или нет доступа к огранизации");
			var avaliableCities = cityProvider.GetCities();
			var model = new OrgModel
			{
				Org = org,
				AvaliableCities = avaliableCities
			};
			var accessType = accessProvider.GetAccessType(HttpContext.GetUserId(), org.ObjectId);
			return accessType == AccessType.Reader ? View("Show", model) : View("Edit", model);
		}

		[HttpPost]
		public ActionResult Edit(OrgEntity org)
		{
			var orgProvider = GetProvider();
			var orgEntity = orgProvider.GetOrg(org.Id);
			org.ObjectId = orgEntity.ObjectId;
			var access = accessProvider.GetAccessType(HttpContext.GetUserId(), org.ObjectId);
			if ((access == AccessType.None || access == AccessType.Reader) && !HttpContext.UserIsAdmin())
				throw new AccessDeniedExpection("Недостаточно прав для редактирования организации");
			orgProvider.UpdateOrganization(org);
			return RedirectToAction("List");
		}

		public bool CanChangeAccess(AccessType accessType)
		{
			return HttpContext.GetUser().Role == AppRoles.Admin || accessType == AccessType.Admin ||
			       accessType == AccessType.Owner;
		}
	}
}