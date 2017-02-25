using System;
using AhovRepository.Entity;

namespace AhovRepository.Repository
{
	public interface IAccessProvider
	{
		AccessType GetAccessType(int userId, string objectId);
		bool AddAccessToObject(int userId, string objectId, int ownerId, AccessType accessType, bool force = false);
		bool RemoveAccessToObject(int userId, string objectId, int ownerId);
	}

	public class AccessProvider : IAccessProvider
	{
		private readonly IDatabaseProvider _databaseProvider;

		public AccessProvider(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public AccessType GetAccessType(int userId, string objectId)
		{
			if (objectId == null)
				return AccessType.None;
			var accessEntity = _databaseProvider.GetOne<AccessEntity>(x => x.User.UserId == userId && x.ObjectId == objectId);
			return accessEntity == null
				? AccessType.None
				: (AccessType) Enum.Parse(typeof(AccessType), accessEntity.AccessType, true);
		}

		public bool AddAccessToObject(int userId, string objectId, int ownerId, AccessType accessType, bool force = false)
		{
			var ownerType = GetAccessType(ownerId, objectId);
			if (ownerType != AccessType.Owner && ownerType != AccessType.Admin && !force)
				return false;
			var userType = GetAccessType(userId, objectId);
			if (userType == AccessType.Owner)
				return false;
			var value = new AccessEntity
			{
				User = new UserEntity {UserId = userId},
				ObjectId = objectId,
				AccessType = accessType.ToString()
			};
			var entity = _databaseProvider.GetOne<AccessEntity>(x => x.User.UserId == userId && x.ObjectId == objectId);
			if (entity == null)
				_databaseProvider.Insert(value);
			else
				_databaseProvider.Update(value);
			return true;
		}

		public bool RemoveAccessToObject(int userId, string objectId, int ownerId)
		{
			var ownerType = GetAccessType(ownerId, objectId);
			if (ownerType != AccessType.Owner || ownerType == AccessType.Admin)
				return false;
			var userType = GetAccessType(userId, objectId);
			if (userType == AccessType.None)
				return true;
			if (userType == AccessType.Owner)
				return false;
			var entity = new AccessEntity
			{
				User = new UserEntity() {UserId = userId},
				ObjectId = objectId
			};
			_databaseProvider.Delete(entity);
			return true;
		}
	}


	public enum AccessType
	{
		None, //не имеет доступа
		Owner, //может делать что хочет и добавлять других
		Admin, //может назначать других в админы, может редактировать
		Reader, //может просматривать
		Editor //может редактировать данные
	}
}