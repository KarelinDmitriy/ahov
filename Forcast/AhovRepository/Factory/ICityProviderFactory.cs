using AhovRepository.Repository;

namespace AhovRepository.Factory
{
	public interface ICityProviderFactory
	{
		ICityProvider CreateCityProvider(int userId);
	}

	public class CityProviderFactory : ICityProviderFactory
	{
		private readonly IAccessProvider _accessProvider;
		private readonly IDatabaseProvider _databaseProvider;

		public CityProviderFactory(IAccessProvider accessProvider,
			IDatabaseProvider databaseProvider)
		{
			_accessProvider = accessProvider;
			_databaseProvider = databaseProvider;
		}

		public ICityProvider CreateCityProvider(int userId)
		{
			return new CityProvider(_accessProvider, _databaseProvider, userId);
		}
	}
}