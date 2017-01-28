using System.Collections.Generic;
using AhovRepository.Entity;

namespace Web.Models.Org
{
	public class OrgModel
	{
		public OrgEntity Org { get; set; }
		public List<CityEntity> AvaliableCities { get; set; }
	}
}