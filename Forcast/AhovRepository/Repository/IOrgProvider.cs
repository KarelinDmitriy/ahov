using System;
using System.Collections.Generic;
using System.Linq;
using AhovRepository.Entity;

namespace AhovRepository.Repository
{
	public interface IOrgProvider
	{
		List<OrgEntity> GetOrgs();
		void AddOrganization(OrgEntity org);
		OrgEntity GetOrg(int orgId);
		OrgEntity GetOrgByAdmin(int orgId);
		void UpdateOrganization(OrgEntity org);
	}

	public class OrgProvider : IOrgProvider
	{
		private readonly IAccessProvider accessProvider;
		private readonly IDatabaseProvider databaseProvider;
		private readonly int userId;

		public OrgProvider(IAccessProvider accessProvider, IDatabaseProvider databaseProvider, int userId)
		{
			this.accessProvider = accessProvider;
			this.databaseProvider = databaseProvider;
			this.userId = userId;
		}

		public List<OrgEntity> GetOrgs()
		{
			var orgs = databaseProvider.GetAll<OrgEntity>();
			return orgs.Where(x => accessProvider.GetAccessType(userId, x.ObjectId) != AccessType.None).ToList();
		}

		public void AddOrganization(OrgEntity org)
		{
			org.ObjectId = Guid.NewGuid().ToString("D");
			databaseProvider.Insert(org);
			var acc = new AccessEntity
			{
				User = new UserEntity {UserId = userId},
				ObjectId = org.ObjectId,
				AccessType = AccessType.Owner.ToString()
			};
			databaseProvider.Insert(acc);
		}

		public OrgEntity GetOrg(int orgId)
		{
			var org = databaseProvider.GetOne<OrgEntity>(x => x.Id == orgId);
			var access = accessProvider.GetAccessType(userId, org.ObjectId);
			return access == AccessType.None ? null : org;
		}

		public OrgEntity GetOrgByAdmin(int orgId)
		{
			return databaseProvider.GetOne<OrgEntity>(x => x.Id == orgId);
		}

		public void UpdateOrganization(OrgEntity org)
		{
			var access = accessProvider.GetAccessType(userId, org.ObjectId);
			if (access != AccessType.Reader && access != AccessType.None)
				databaseProvider.Update(org);
		}
	}
}