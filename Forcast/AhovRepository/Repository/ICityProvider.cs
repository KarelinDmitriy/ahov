using System.Collections.Generic;
using AhovRepository.Entity;

namespace AhovRepository.Repository
{
	public interface ICityProvider
	{
		List<CityEntity> GetCities();
		void AddCity(CityEntity entity);
		CityEntity GetCity(int cityId);
		void UpdateCity(CityEntity entity);
		void UpdateCityType(CityTypeEntity entity);
		void AddCityType(CityTypeEntity cityType);
	}

	public class CityProvider : ICityProvider
	{
		private readonly IAccessProvider _access;
		private readonly IDatabaseProvider _databaseProvider;
		private readonly int _userId;

		public CityProvider(IAccessProvider access,
			IDatabaseProvider databaseProvider,
			int userId)
		{
			_access = access;
			_databaseProvider = databaseProvider;
			_userId = userId;
		}


		public List<CityEntity> GetCities()
		{
			return _databaseProvider.GetAll<CityEntity>();
		}

		public void AddCity(CityEntity entity)
		{
			_databaseProvider.Insert(entity);
		}

		public CityEntity GetCity(int cityId)
		{
			return _databaseProvider.GetOne<CityEntity>(x => x.CityId == cityId);
		}

		public void UpdateCity(CityEntity entity)
		{
			_databaseProvider.Update(entity);
		}

		public void UpdateCityType(CityTypeEntity entity)
		{
			_databaseProvider.Update(entity);
		}

		public void AddCityType(CityTypeEntity cityType)
		{
			_databaseProvider.Insert(cityType);
		}
	}
}