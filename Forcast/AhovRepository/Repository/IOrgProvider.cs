using System.Collections.Generic;
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
			return _databaseProvider.GetAll<OrgEntity>();
		}

		public void AddOrganization(OrgEntity org)
		{
			_databaseProvider.Insert(org);
		}

		public OrgEntity GetOrg(int orgId)
		{
			return _databaseProvider.GetOne<OrgEntity>(x => x.Id == orgId);
		}

		public void UpdateOrganization(OrgEntity org)
		{
			_databaseProvider.Update(org);
		}
	}
}