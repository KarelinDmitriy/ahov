using System.Web.Mvc;
using AhovRepository;
using Web.Models.Org;

namespace Web.Controllers
{
	public class OrgController : Controller
	{
		private readonly IAhovRepository _repository;
		public OrgController(IAhovRepository repository)
		{
			_repository = repository;
		}

		public ActionResult List()
		{
			var orgs = _repository.GetOrganizations(1);

			return View(orgs);
		}

		public ActionResult Create()
		{
			var org = new WebOrganization();
			var avaliableCities = _repository.GetAvalibleCity(1);
			var model = new OrgModel
			{
				Org = org,
				AvaliableCities = avaliableCities
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(WebOrganization org)
		{
			_repository.AddOrganization(org, 1);
			return RedirectToAction("List");
		}

		public ActionResult Edit(int orgId)
		{
			var org = _repository.GetOrganization(orgId, 1);
			var avaliableCities = _repository.GetAvalibleCity(1);
			var model = new OrgModel
			{
				Org = org,
				AvaliableCities = avaliableCities
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(WebOrganization org)
		{
			_repository.UpdateOrganizatino(org, 1);
			return RedirectToAction("List");
		}
	}
}