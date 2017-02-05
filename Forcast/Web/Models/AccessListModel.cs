using System.Collections.Generic;
using AhovRepository.Entity;

namespace Web.Controllers
{
	public class AccessListModel
	{
		public OrgEntity Org { get; set; }

		public List<AccessEntity> Access { get; set; }
	}
}