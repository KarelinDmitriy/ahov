using System.Web.Mvc;
using AhovRepository.Entity;
using Web.Core;

namespace Web.Controllers
{
	public class AppBaseController : Controller
	{
		protected AppUser GetUser()
		{
			return HttpContext.User.Identity as AppUser;
		}
	}
}