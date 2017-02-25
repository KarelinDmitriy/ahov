using System.Collections.Generic;
using AhovRepository.Entity;

namespace Web.Models.Org
{
	public class ListOrgModel
	{
		public List<OrgItem> Items { get; set; }
	}

	public class OrgItem
	{
		public OrgEntity Org { get; set; }
		public bool CanChangeAccess { get; set; }
	}
}