using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using Web.Core;
using Web.Models.User;
using static Web.Core.Helpers.PasswordHelper;

namespace Web.Controllers
{
	public class UserController : AppBaseController
	{
		private readonly IDatabaseProvider repository;

		public UserController(IDatabaseProvider repository)
		{
			this.repository = repository;
		}

		public ActionResult Edit(int userId)
		{
			var httpUser = GetUser();
			if (userId != httpUser.UserId)
				throw new NotSupportedException();
			var user = repository.GetOne<UserEntity>(x => x.UserId == userId);
			user.PasswordHash = string.Empty;
			var model = new UserModel()
			{
				Info = user
			};
			return View(model);
		}

		
		[HttpPost]
		public ActionResult Edit(UserModel model)
		{
			var httpUser = GetUser();
			if (model.Info.UserId != httpUser.UserId)
				throw new NotSupportedException();
			var user = repository.GetOne<UserEntity>(x => x.UserId == model.Info.UserId);
			user.Email = model.Info.Email;
			user.Fio = model.Info.Fio;
			repository.Update(user);
			return RedirectToAction("Edit", "User", new {userId = model.Info.UserId});
		}

		[HttpPost]
		public ActionResult ChangePass(UserModel model)
		{
			var httpUser = GetUser();
			if (model.Info.UserId != httpUser.UserId)
				throw new NotSupportedException();
			var user = repository.GetOne<UserEntity>(x => x.UserId == model.Info.UserId);
			if (user.PasswordHash != HashPassowrd(model.Password))
				throw new NotSupportedException();
			user.PasswordHash = HashPassowrd(model.NewPassowrd);
			repository.Update(user);
			return RedirectToAction("Edit", "User", new {userId = model.Info.UserId});
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
			var info = repository.GetOne<UserEntity>(x => x.Login == login);
			if (info == null)
			{
				ViewData["error"] = $"Пользователь с именем {login} не найден";
				return RedirectToAction("Login");
			}
			if (info.PasswordHash != HashPassowrd(userModel.Password))
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
			model.Info.Role = AppRoles.Client;
			model.Info.PasswordHash = HashPassowrd(model.Password);
			repository.Insert(model.Info);
			AuthorizeUser(model.Info.Login);
			return RedirectToAction("Index", "Home");
		}

		[AppAuthorize(AppRoles.Admin)]
		public ActionResult List()
		{
			var users = repository.GetAll<UserEntity>().Select(x => new UserModel(x)).ToList();
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