using System.Web.Mvc;
using AhovRepository;

namespace Web.Controllers
{
	public class BarrelController : Controller
	{
		private readonly IDatabaseProvider _repository;

		public BarrelController(IDatabaseProvider repository)
		{
			_repository = repository;
		}
		
	}
}