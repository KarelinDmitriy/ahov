using System.Collections.Generic;
using System.ComponentModel;
using FluentNHibernate.Mapping;

namespace AhovRepository.Entity
{
	public class CityEntity
	{
		public virtual int CityId { get; set; }
		[DisplayName("Название города")]
		public virtual string Name { get; set; }
		[DisplayName("Длинна города (м)")]
		public virtual double Lenght { get; set; }
		[DisplayName("Ширина города (м)")]
		public virtual double Width { get; set; }
		[DisplayName("Численность населения")]
		public virtual int Population { get; set; }
		[DisplayName("Процент детей")]
		public virtual int ChildPercent { get; set; }
		[DisplayName("Доля занимающих укрытие")]
		public virtual double Au { get; set; }
		[DisplayName("Доля занимающих укрытие (дети)")]
		public virtual double AuChild { get; set; }
		[DisplayName("Для выходящих с зараженной местности")]
		public virtual double Aw { get; set; }
		[DisplayName("Для выходящих с зараженной местности (дети)")]
		public virtual double AwChild { get; set; }
		[DisplayName("Для применяющих антидоты")]
		public virtual double Aa { get; set; }
		[DisplayName("Для применяющих антидоты (дети)")]
		public virtual double AaChild { get; set; }
		[DisplayName("Доля применяющих СИЗ")]
		public virtual double Apr { get; set; }
		[DisplayName("Доля применяющих СИЗ (дети)")]
		public virtual double AprChild { get; set; }
		public virtual ICollection<CityTypeEntity> CityBuildings { get; set; }
	}

	public class CityMap : ClassMap<CityEntity>
	{
		public CityMap()
		{
			Id(x => x.CityId).Column("CityId");
			Map(x => x.Name).Column("Name");
			Map(x => x.Lenght).Column("Lenght");
			Map(x => x.Width).Column("Width");
			Map(x => x.Population).Column("Population");
			Map(x => x.ChildPercent).Column("ChildPercent");
			Map(x => x.Au).Column("Au");
			Map(x => x.AuChild).Column("Au_ch");
			Map(x => x.Aw).Column("Aw");
			Map(x => x.AwChild).Column("Aw_ch");
			Map(x => x.Aa).Column("Aa");
			Map(x => x.AaChild).Column("Aa_ch");
			Map(x => x.Apr).Column("Apr");
			Map(x => x.AprChild).Column("Apr_ch");
			HasMany(x => x.CityBuildings)
				.KeyColumn("BuildingId");
			Table("City");
		}
	}
}