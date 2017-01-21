using AhovRepository.Repository;

namespace AhovRepository.Factory
{
	public interface IOrgDataproviderFactory
	{
		IOrgProvider CreateOrgProvider(int userId);
	}

	public class OrgDataproviderFactory : IOrgDataproviderFactory
	{
		private readonly IAccessProvider _accessProvider;
		private readonly IDatabaseProvider _databaseProvider;

		public OrgDataproviderFactory(IAccessProvider accessProvider, IDatabaseProvider databaseProvider)
		{
			_accessProvider = accessProvider;
			_databaseProvider = databaseProvider;
		}

		public IOrgProvider CreateOrgProvider(int userId)
		{
			return new OrgProvider(_accessProvider, _databaseProvider, userId);
		}
	}
}