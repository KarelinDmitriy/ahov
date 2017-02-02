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
		void UpdateOrganization(OrgEntity org);
	}

	public class OrgProvider : IOrgProvider
	{
		private readonly IAccessProvider _accessProvider;
		private readonly IDatabaseProvider _databaseProvider;
		private readonly int _userId;

		public OrgProvider(IAccessProvider accessProvider, IDatabaseProvider databaseProvider, int userId)
		{
			_accessProvider = accessProvider;
			_databaseProvider = databaseProvider;
			_userId = userId;
		}

		public List<OrgEntity> GetOrgs()
		{
			var orgs = _databaseProvider.GetAll<OrgEntity>();
			return orgs.Where(x => _accessProvider.GetAccessType(_userId, x.ObjectId) != AccessType.None).ToList();
		}

		public void AddOrganization(OrgEntity org)
		{
			org.ObjectId = Guid.NewGuid().ToString("D");
			_databaseProvider.Insert(org);
			var acc = new AccessEntity
			{
				User = new UserEntity {UserId = _userId},
				ObjectId = org.ObjectId,
				AccessType = AccessType.Owner.ToString()
			};
			_databaseProvider.Insert(acc);
		}

		public OrgEntity GetOrg(int orgId)
		{
			var org = _databaseProvider.GetOne<OrgEntity>(x => x.Id == orgId);
			var access = _accessProvider.GetAccessType(_userId, org.ObjectId);
			return access == AccessType.None ? null : org;
		}

		public void UpdateOrganization(OrgEntity org)
		{
			var access = _accessProvider.GetAccessType(_userId, org.ObjectId);
			if (access != AccessType.Reader && access != AccessType.None)
				_databaseProvider.Update(org);
		}
	}
}