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
		CityTypeEntity AddCityType(CityTypeEntity cityType);
		List<CityTypeEntity> GetBuilding(int cityId);
		void DeleteCityType(int cityTypeId);
	}

	public class CityProvider : ICityProvider
	{
		private readonly IDatabaseProvider _databaseProvider;

		public CityProvider(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
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

		public CityTypeEntity AddCityType(CityTypeEntity cityType)
		{
			return _databaseProvider.Insert(cityType);
		}

		public List<CityTypeEntity> GetBuilding(int cityId)
		{
			return _databaseProvider.Where<CityTypeEntity>(x => x.City.CityId == cityId);
		}

		public void DeleteCityType(int cityTypeId)
		{
			var entity = new CityTypeEntity {Id = cityTypeId};
			_databaseProvider.Delete(entity);
		}
	}
}