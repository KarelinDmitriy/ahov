using FluentNHibernate.Mapping;

namespace AhovRepository.Entity
{
	public class AccessEntity
	{
		public virtual string ObjectId { get; set; }
		public virtual UserEntity User { get; set; }
		public virtual string AccessType { get; set; }

		public override bool Equals(object obj)
		{
			var b = obj as AccessEntity;
			if (b == null)
				return false;
			return ObjectId == b.ObjectId && User.UserId == b.User.UserId;
		}

		protected bool Equals(AccessEntity other)
		{
			return string.Equals(ObjectId, other.ObjectId) && Equals(User, other.User);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((ObjectId?.GetHashCode() ?? 0)*397) ^ (User?.GetHashCode() ?? 0);
			}
		}
	}

	public class AccessMap : ClassMap<AccessEntity>
	{
		public AccessMap()
		{
			CompositeId()
				.KeyProperty(x => x.ObjectId, "ObjectId")
				.KeyReference(x => x.User, "UserId");
			Map(x => x.AccessType).Column("AccessType");
			Table("AccessTable");
		}
	}
}