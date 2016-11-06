﻿using System.Collections.Generic;

namespace AhovRepository
{
	public interface IAhovRepository
	{
		List<WebUser> GetAllUsers();
		void AddUser(WebUser user, string password);
		List<WebCity> GetAvalibleCity(int userId);
		void AddNewCity(WebCity city);
		WebCity GetCity(int cityId, int userId);
		void UpdateCity(WebCity city, int userId);
		void UpdateCityType(CityType cityType, int userId);
		void AddNewCityType(CityType cityType, int userId);
	}
}
