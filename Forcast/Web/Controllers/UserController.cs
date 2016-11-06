using System.Web.Mvc;
using AhovRepository;
using Web.Models.User;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		private readonly IAhovRepository _repository;

		public UserController(IAhovRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Create()
		{
			var model = new UserModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(UserModel model)
		{
			_repository.AddUser(model.Info, model.Password);
			return RedirectToAction("List");
		}

		public ActionResult List()
		{
			var users = _repository.GetAllUsers();
			return View(users);
		}
	}
}