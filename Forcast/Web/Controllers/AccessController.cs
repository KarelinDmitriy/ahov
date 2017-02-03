using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AhovRepository.Entity;
using AhovRepository.Factory;
using Web.Core;

namespace Web.Controllers
{
	[AppAuthorize]
	public class AccessController : Controller
	{
		private readonly IOrgDataproviderFactory orgProdiver;

		public AccessController(IOrgDataproviderFactory orgProdiver)
		{
			this.orgProdiver = orgProdiver;
		}

		public ActionResult AccessList(int orgId)
		{
			var org = orgProdiver.CreateOrgProvider(HttpContext.GetUserId()).GetOrg(orgId);
			if (org == null)
				throw new NotImplementedException();

			var model = new AccessListModel
			{
				Org = org
			};
			return View(model);
		}

	}

	public class AccessListModel
	{
		public OrgEntity Org { get; set; }
	}
}