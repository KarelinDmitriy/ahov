using System.Collections.Generic;
using AhovRepository.Entity;

namespace Web.Models.City
{
	public class ListCitiesModel
	{
		public List<CityEntity> Cities { get; set; }
		public bool CanCreateCity { get; set; }
	}
}