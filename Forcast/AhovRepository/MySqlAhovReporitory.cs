using System.Linq;

namespace AhovRepository
{
	public class MySqlAhovReporitory : IAhovRepository
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


	}
}