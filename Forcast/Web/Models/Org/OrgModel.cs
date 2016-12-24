using System.Collections.Generic;
using AhovRepository;

namespace Web.Models.Org
{
	public class OrgModel
	{
		public WebOrganization Org { get; set; }
		public List<WebCity> AvaliableCities { get; set; }
	}
}