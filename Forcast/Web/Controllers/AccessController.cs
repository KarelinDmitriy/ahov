using System;
using System.Linq;
using System.Web.Mvc;
using AhovRepository;
using AhovRepository.Entity;
using AhovRepository.Factory;
using AhovRepository.Repository;
using Web.Core;
using Web.Models;

namespace Web.Controllers
{
	[AppAuthorize]
	public class AccessController : Controller
	{
		private readonly IOrgDataproviderFactory orgProdiver;
		private readonly IDatabaseProvider databaseProvider;
		private readonly IAccessProvider accessProvider;

		public AccessController(IOrgDataproviderFactory orgProdiver,
			IDatabaseProvider databaseProvider,
			IAccessProvider accessProvider)
		{
			this.orgProdiver = orgProdiver;
			this.databaseProvider = databaseProvider;
			this.accessProvider = accessProvider;
		}

		public ActionResult AccessList(int orgId)
		{
			var org = orgProdiver.CreateOrgProvider(HttpContext.GetUserId()).GetOrg(orgId);
			if (org == null)
				throw new NotImplementedException();

			var access = databaseProvider.Where<AccessEntity>(x => x.ObjectId == org.ObjectId)
				.Select(x => new AccessEntity
				{
					ObjectId = x.ObjectId,
					AccessType = x.AccessType,
					User = databaseProvider.GetOne<UserEntity>(y => y.UserId == x.User.UserId)
				})
				.ToList();

			var model = new AccessListModel
			{
				Org = org,
				Access = access
			};
			return View("List", model);
		}

		[HttpPost]
		public ActionResult AddOrCreate(AddAccessModel model)
		{
			var userAccType = accessProvider.GetAccessType((User.Identity as AppUser).UserId, model.ObjectId);
			if (!(userAccType == AccessType.Owner || userAccType == AccessType.Admin))
				return PartialView(new AccessResultModel
				{
					Reason = "Недостаточно прав",
					Success = false
				});

			var user = databaseProvider.GetOne<UserEntity>(x => x.Login == model.Login);
			if (user == null)
				return PartialView(new AccessResultModel
				{
					Reason = "Пользователь не существует",
					Success = false
				});

			var access = accessProvider.GetAccessType(user.UserId, model.ObjectId);
			if (access == AccessType.Owner)
				return PartialView(new AccessResultModel
				{
					Reason = "Недостаточно прав",
					Success = false
				});
			accessProvider.AddAccessToObject(user.UserId, model.ObjectId, (User.Identity as AppUser).UserId, (AccessType) Enum.Parse(typeof(AccessType), model.AccessType, true));
			return PartialView(new AccessResultModel
			{
				Reason = "",
				Success = true
			});
		}

	}
}