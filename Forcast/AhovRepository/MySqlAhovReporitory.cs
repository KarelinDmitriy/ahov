using System.Collections.Generic;
using System.Linq;

namespace AhovRepository
{
	public partial class MySqlAhovReporitory : IAhovRepository
	{
		private readonly Ahov_Connection _connection;

		public MySqlAhovReporitory(Ahov_Connection connection)
		{
			_connection = connection;
		}

		public WebUser GetUser(int id)
		{
			var user = _connection.user.FirstOrDefault(x => x.UserId == id);
			if (user == null)
				return null;
			return new WebUser
			{
				Id = user.UserId,
				Email = user.Email,
				Login = user.UserName,
				Role = user.Role
			};
		}

		public List<WebUser> GetAllUsers()
		{
			return _connection.user
				.Select(x => new WebUser {Role = x.Role, Email = x.Email, Login = x.UserName, Id = x.UserId})
				.ToList();
		}

		public void AddUser(WebUser user, string password)
		{
			var insertUser = new user {Email = user.Email, Role = 0, UserName = user.Login, PasswordHash = password};
			_connection.user.Add(insertUser);
			_connection.SaveChanges();
		}

		//TODO:Сделать фильтрацию
		public List<WebCity> GetAvalibleCity(int userId)
		{
			return _connection.city
				.Select(city => new WebCity
				{
					Id = city.CityId,

					Name = city.Name,
					Population = city.Population,
					ChildPrecent = city.ChildPercent
				})
				.ToList();
		}

		public void AddNewCity(WebCity city)
		{
			_connection.city.Add(new city
			{
				Name = city.Name,
				Lenght = city.Lenght,
				Width = city.Width,
				Population = city.Population,
				ChildPercent = city.ChildPrecent,

				Aa = city.Aa,
				Au = city.Au,
				Apr = city.Apr,
				Aw = city.Aw,

				Aa_ch = city.AaChild,
				Apr_ch = city.AprChild,
				Au_ch = city.AuChild,
				Aw_ch = city.AwChild,
			});
			_connection.SaveChanges();
		}

		public WebCity GetCity(int cityId, int userId)
		{
			var city = _connection.city.FirstOrDefault(x => x.CityId == cityId);
			if (city == null)
				return null;
			return new WebCity
			{
				Id = cityId,
				Name = city.Name,
				Lenght = city.Lenght,
				Width = city.Width,
				Population = city.Population,
				ChildPrecent = city.ChildPercent,

				Aa = city.Aa,
				Au = city.Au,
				Apr = city.Apr,
				Aw = city.Aw,

				AaChild = city.Aa_ch,
				AprChild = city.Apr_ch,
				AuChild = city.Au_ch,
				AwChild = city.Aw_ch,

				CityTypes = city.citybuilding.Select(x => new CityType
				{
					CityId = x.City_CityId,
					Kp = x.Kp,
					Name = x.TypeName,
					Ay = x.Ay,
					Id = x.BuildingId
				}).ToList()
			};
		}

		public void UpdateCity(WebCity city, int userId)
		{
			var cityBase = _connection.city.FirstOrDefault(x => x.CityId == city.Id);
			if (cityBase == null)
				return;
			cityBase.Name = city.Name;
			cityBase.Lenght = city.Lenght;
			cityBase.Width = city.Width;
			cityBase.Population = city.Population;
			cityBase.ChildPercent = city.ChildPrecent;
			cityBase.Aa = city.Aa;
			cityBase.Au = city.Au;
			cityBase.Apr = city.Apr;
			cityBase.Aw = city.Aw;
			cityBase.Aa_ch = city.AaChild;
			cityBase.Apr_ch = city.AprChild;
			cityBase.Au_ch = city.AuChild;
			cityBase.Aw_ch = city.AwChild;

			_connection.SaveChanges();
		}

		public void UpdateCityType(CityType cityType, int userId)
		{
			var cityBuilding = _connection.citybuilding.FirstOrDefault(x => x.BuildingId == cityType.Id);
			if (cityBuilding == null)
				return;

			cityBuilding.Kp = cityType.Kp;
			cityBuilding.Ay = cityType.Ay;
			cityBuilding.TypeName = cityType.Name;

			_connection.SaveChanges();
		}

		public void AddNewCityType(CityType cityType, int userId)
		{
			_connection.citybuilding.Add(new citybuilding
			{
				Kp = cityType.Kp,
				Ay = cityType.Ay,
				TypeName = cityType.Name,
				City_CityId = cityType.CityId
			});
			_connection.SaveChanges();
		}
	}
}