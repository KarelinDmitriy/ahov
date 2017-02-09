using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using Web.Core;
using Web.Models.Admin;

namespace Web.Controllers.Admin
{
	[AppAuthorize("Admin")]
	public class AdminController : AppBaseController
	{
		private readonly IDatabaseProvider _databaseProvider;

		public AdminController(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public ActionResult Menu()
		{
			return View();
		}

		public ActionResult UserList()
		{
			var users = _databaseProvider.GetAll<UserEntity>();
			var model = new UserListModel
			{
				Users = users
			};
			return View(model);
		}

		public ActionResult EditUser(int userId)
		{
			var user = _databaseProvider.GetOne<UserEntity>(x => x.UserId == userId);
			var userModel = new AdminUserModel {User = user};
			return View(userModel);
		}

		[HttpPost]
		public ActionResult EditUser(AdminUserModel model)
		{
			var dbUser = _databaseProvider.GetOne<UserEntity>(x => x.UserId == model.User.UserId);
			dbUser.Email = model.User.Email;
			dbUser.Fio = model.User.Fio;
			dbUser.Role = model.User.Role;
			_databaseProvider.Update(dbUser);
			return RedirectToAction("UserList");
		}
	}
}