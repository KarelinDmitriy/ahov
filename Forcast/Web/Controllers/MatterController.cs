using System.Web.Mvc;
using AhovRepository;

namespace Web.Controllers
{
	public class MatterController : Controller
	{
		private readonly IAhovRepository _repository;

		public MatterController(IAhovRepository repository)
		{
			_repository = repository;
		}
	}
}