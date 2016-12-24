using System.Web.Mvc;
using AhovRepository;

namespace Web.Controllers
{
	public class BarrelController : Controller
	{
		private readonly IAhovRepository _repository;

		public BarrelController(IAhovRepository repository)
		{
			_repository = repository;
		}
		
	}
}