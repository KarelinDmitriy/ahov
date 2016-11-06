using System.Web.Mvc;
using AhovRepository;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAhovRepository _repository;

		public HomeController(IAhovRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}