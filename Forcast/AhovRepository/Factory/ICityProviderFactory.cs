using AhovRepository.Entity;
using AhovRepository.Repository;

namespace AhovRepository.Factory
{
	public interface ICityProviderFactory
	{
		ICityProvider CreateCityProvider(int userId);
	}

	public class CityProviderFactory : ICityProviderFactory
	{
		private readonly IDatabaseProvider _databaseProvider;

		public CityProviderFactory(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public ICityProvider CreateCityProvider(int userId)
		{
			var user = _databaseProvider.GetOne<UserEntity>(x => x.UserId == userId);
			return new CityProvider(_databaseProvider, user);
		}
	}
}