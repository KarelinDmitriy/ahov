using AhovRepository.Repository;

namespace AhovRepository.Factory
{
	public interface IBarrelProviderFactory
	{
		IBarrelProvider CreateBarrelProvider(int userId);
	}

	public class BarrelProviderFactory : IBarrelProviderFactory
	{
		private readonly IAccessProvider _accessProvider;
		private readonly IDatabaseProvider _databaseProvider;

		public BarrelProviderFactory(IAccessProvider accessProvider, IDatabaseProvider databaseProvider)
		{
			_accessProvider = accessProvider;
			_databaseProvider = databaseProvider;
		}

		public IBarrelProvider CreateBarrelProvider(int userId)
		{
			return new BarrelProvider(_databaseProvider, _accessProvider, userId);
		}
	}
}