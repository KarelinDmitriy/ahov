using System.ComponentModel;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;

namespace AhovRepository.Entity
{
	public class BarrelEntity
	{
		public virtual int BarrelId { get; set; }
		[DisplayName("Обозначение")]
		public virtual string Name { get; set; }
		[DisplayName("Вещество")]
		public virtual string Code { get; set; }
		[DisplayName("")]
		public virtual string Draining { get; set; }
		[DisplayName("Тип хранения")]
		public virtual string SaveType { get; set; }
		[DisplayName("Кол-во вещества")]
		public virtual double Q { get; set; }
		[DisplayName("Высота поддона")]
		public virtual double H { get; set; }
		public virtual OrgEntity Org { get; set; }
	}

	public class BarrelMap : ClassMap<BarrelEntity>
	{
		public BarrelMap()
		{
			Id(x => x.BarrelId).Column("BarrelId");
			Map(x => x.Name).Column("Name");
			Map(x => x.Draining).Column("Draining");
			Map(x => x.SaveType).Column("SaveType");
			Map(x => x.Q).Column("Q");
			Map(x => x.H).Column("H");
			Map(x => x.Code).Column("Code");
			References(x => x.Org)
				.Columns("OrgId");
			Table("CityBuilding");
		}
	}
}