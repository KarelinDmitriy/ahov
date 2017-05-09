using System.ComponentModel;
using System.Web.Mvc;
using FluentNHibernate.Mapping;

namespace AhovRepository.Entity
{
	public class CityEntity
	{
		[DisplayName("Город")]
		public virtual int CityId { get; set; }

		[DisplayName("Название города")]
		public virtual string Name { get; set; }

		[DisplayName("Длинна города")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Lenght { get; set; }

		[DisplayName("Ширина города (м)")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Width { get; set; }

		[DisplayName("Численность населения")]
		[AdditionalMetadata("Dimension", "чел.")]
		public virtual int Population { get; set; }

		[DisplayName("Процент детей")]
		[AdditionalMetadata("Dimension", "%")]
		public virtual int ChildPercent { get; set; }

		[DisplayName("Доля занимающих укрытие")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Au { get; set; }

		[DisplayName("Доля занимающих укрытие (дети)")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double AuChild { get; set; }

		[DisplayName("Для выходящих с зараженной местности")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Aw { get; set; }

		[DisplayName("Для выходящих с зараженной местности (дети)")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double AwChild { get; set; }

		[DisplayName("Для применяющих антидоты")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Aa { get; set; }

		[DisplayName("Для применяющих антидоты (дети)")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double AaChild { get; set; }

		[DisplayName("Доля применяющих СИЗ")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Apr { get; set; }

		[DisplayName("Доля применяющих СИЗ (дети)")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double AprChild { get; set; }
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
			Table("City");
		}
	}
}