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
		List<CityTypeEntity> GetBuilding(int cityId);
	}

	public class CityProvider : ICityProvider
	{
		private readonly IDatabaseProvider _databaseProvider;
		private readonly UserEntity _user;

		public CityProvider(IDatabaseProvider databaseProvider,
			UserEntity user)
		{
			_databaseProvider = databaseProvider;
			_user = user;
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

		public List<CityTypeEntity> GetBuilding(int cityId)
		{
			return _databaseProvider.Where<CityTypeEntity>(x => x.City.CityId == cityId);
		}
	}
}