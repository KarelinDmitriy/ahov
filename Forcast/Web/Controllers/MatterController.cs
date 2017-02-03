using System.Web.Mvc;
using AhovRepository;

namespace Web.Controllers
{
	public class MatterController : Controller
	{
		private readonly IDatabaseProvider repository;

		public MatterController(IDatabaseProvider repository)
		{
			this.repository = repository;
		}
	}
}