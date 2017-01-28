using System.Collections.Generic;
using AhovRepository.Entity;

namespace Web.Models.City
{
	public class CityModel
	{
		public CityModel()
		{
			City = new CityEntity();
			Buildings = new List<CityTypeEntity>();
		}

		public CityEntity City { get; set; }
		public List<CityTypeEntity> Buildings { get; set; }
	}
}