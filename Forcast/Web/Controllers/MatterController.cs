using System.Web.Mvc;
using AhovRepository;

namespace Web.Controllers
{
	public class MatterController : Controller
	{
		private readonly IDatabaseProvider _repository;

		public MatterController(IDatabaseProvider repository)
		{
			_repository = repository;
		}
	}
}