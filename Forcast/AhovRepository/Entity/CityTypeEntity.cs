using System.ComponentModel;
using System.Web.Mvc;
using FluentNHibernate.Mapping;

namespace AhovRepository.Entity
{
	public class CityTypeEntity
	{
		public virtual int Id { get; set; }
		public virtual CityEntity City { get; set; }

		[DisplayName("Тип сооружения")]
		public virtual string Name { get; set; }

		[DisplayName("Коэффициент проникнования")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Kp { get; set; }

		[DisplayName("Доля населения")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Ay { get; set; }
	}

	public class CityTypeMap : ClassMap<CityTypeEntity>
	{
		public CityTypeMap()
		{
			Id(x => x.Id).Column("BuildingId");
			Map(x => x.Name).Column("TypeName");
			Map(x => x.Ay).Column("Ay");
			Map(x => x.Kp).Column("Kp");
			References(x => x.City)
				.Columns("City_CityId");
			Table("CityBuilding");
		}
	}
}