using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using Web.Core;
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

		public ActionResult Login()
		{
			var model = new UserModel();
			return View(model);
		}

		public ActionResult LogOut()
		{
			var cookie = new HttpCookie(CookiesConst.UserInfo, string.Empty);
			var cookieForTime = new HttpCookie(CookiesConst.Time, DateTime.MinValue.ToString());
			HttpContext.Response.SetCookie(cookie);
			HttpContext.Response.SetCookie(cookieForTime);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult Login(UserModel userModel)
		{
			var login = userModel.Info.Login;
			var info = _repository.GetOne<UserEntity>(x => x.Login == login);
			if (info == null)
			{
				ViewData["error"] = $"Пользователь с именем {login} не найден";
				return RedirectToAction("Login");
			}
			if (info.PasswordHash != userModel.Password)
			{
				ViewData["error"] = $"Не верный пароль";
				return RedirectToAction("Login");
			}
			AuthorizeUser(info.Login);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult Create(UserModel model)
		{
			model.Info.PasswordHash = model.Password;
			model.Info.Role = AppRules.Clinet;
			_repository.Insert(model.Info);
			AuthorizeUser(model.Info.Login);
			return RedirectToAction("Index", "Home");
		}

		public ActionResult List()
		{
			var users = _repository.GetAll<UserEntity>().Select(x => new UserModel(x)).ToList();
			return View(users);
		}

		private void AuthorizeUser(string name)
		{
			var cookie = new HttpCookie(CookiesConst.UserInfo, name);
			var cookieForTime = new HttpCookie(CookiesConst.Time, DateTime.UtcNow.AddHours(3).ToString());
			HttpContext.Response.SetCookie(cookie);
			HttpContext.Response.SetCookie(cookieForTime);
		}
	}
}