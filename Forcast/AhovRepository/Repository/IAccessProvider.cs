namespace AhovRepository.Repository
{
	public interface IAccessProvider
	{
		bool HasAccessToObject(int userId, string objectId);
		void AddAccessToObject(int userId, string objectId, int ownerId);
		void RemoveAccessToObject(int userId, string objectId, int ownerId);
	}

	public class FakeAccessProvider : IAccessProvider
	{
		public bool HasAccessToObject(int userId, string objectId)
		{
			return true;
		}

		public void AddAccessToObject(int userId, string objectId, int ownerId)
		{
			
		}

		public void RemoveAccessToObject(int userId, string objectId, int ownerId)
		{
			
		}
	}

	public enum AccessType
	{
		Owner, //может делать что хочет и добавлять других
		Reader, //может просматривать
		Editor //может редактировать данные
	}
}