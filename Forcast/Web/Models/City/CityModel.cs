using System.Collections.Generic;
using AhovRepository.Entity;

namespace Web.Models.City
{
	public class CityModel
	{
		public CityEntity City { get; set; }
		public List<CityTypeEntity> Buildings { get; set; }
	}
}