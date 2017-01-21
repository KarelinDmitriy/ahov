using System.Linq;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using Web.Models.User;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		private readonly IDatabaseProvider _repository;

		public UserController(IDatabaseProvider repository)
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
			model.Info.PasswordHash = model.Password;
			_repository.Insert(model.Info);
			return RedirectToAction("List");
		}

		public ActionResult List()
		{
			var users = _repository.GetAll<UserEntity>().Select(x => new UserModel(x)).ToList();
			return View(users);
		}
	}
}