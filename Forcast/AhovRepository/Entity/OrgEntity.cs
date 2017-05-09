using System.ComponentModel;
using System.Web.Mvc;
using FluentNHibernate.Mapping;

namespace AhovRepository.Entity
{
	public class OrgEntity
	{
		public virtual int Id { get; set; }

		public virtual CityEntity City { get; set; }

		[DisplayName("Название организации")]
		public virtual string Name { get; set; }

		[DisplayName("Численность персонала")]
		public virtual int PersonalCount { get; set; }

		[DisplayName("Коэффициент физической нагрузки")]
		public virtual double Kf { get; set; }

		[DisplayName("Радиус объекта")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Ro { get; set; }

		[DisplayName("Длинна объекта")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Length { get; set; }

		[DisplayName("Ширина объекта")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Width { get; set; }

		[DisplayName("Радиус санитарной зоны")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Rz { get; set; }

		[DisplayName("Время одевания СИЗ")]
		[AdditionalMetadata("Dimension", "с")]
		public virtual double Top { get; set; }

		[DisplayName("Время выхода")]
		[AdditionalMetadata("Dimension", "с")]
		public virtual double Tow { get; set; }

		[DisplayName("Растояние до леса")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Gdl { get; set; }

		[DisplayName("Глубина леса")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double Gl { get; set; }

		[DisplayName("Высота возвышенностей")]
		[AdditionalMetadata("Dimension", "м")]
		public virtual double W { get; set; }

		[DisplayName("Доля применяющих антидоты")]
		[AdditionalMetadata("Dimension", "ед.")]
		public virtual double Aao { get; set; }

		[DisplayName("Скорость выхода с зараженной зоны")]
		public virtual double Bw { get; set; }

		[DisplayName("Скорость выхода (для детей)")]
		public virtual double Bw_ch { get; set; }

		[DisplayName("Скорость занятий укрытий")]
		public virtual double Bu { get; set; }

		[DisplayName("Скорость занятий укрытий (для детей)")]
		public virtual double Bu_ch { get; set; }

		[DisplayName("Время принятия антидотов")]
		public virtual double Ba { get; set; }

		[DisplayName("Время принятия антидотов (для детей)")]
		public virtual double Ba_ch { get; set; }

		public virtual string ObjectId { get; set; }
	}

	public class OrgMap : ClassMap<OrgEntity>
	{
		public OrgMap()
		{
			Id(x => x.Id).Column("OrgId");
			Map(x => x.Name).Column("OgranizationName");
			Map(x => x.PersonalCount).Column("PersonalCount");
			Map(x => x.Length).Column("Length");
			Map(x => x.Width).Column("Width");
			Map(x => x.Kf).Column("Kf");
			Map(x => x.Ro).Column("Ro");
			Map(x => x.Rz).Column("Rz");
			Map(x => x.Top).Column("Top");
			Map(x => x.Tow).Column("Tow");
			Map(x => x.Gdl).Column("Gdl");
			Map(x => x.Gl).Column("Gl");
			Map(x => x.W).Column("W");
			Map(x => x.Aao).Column("Aao");
			Map(x => x.Bw).Column("Bw");
			Map(x => x.Bw_ch).Column("Bw_ch");
			Map(x => x.Bu).Column("Bu");
			Map(x => x.Bu_ch).Column("Bu_ch");
			Map(x => x.Ba).Column("Ba");
			Map(x => x.Ba_ch).Column("Ba_ch");
			Map(x => x.ObjectId).Column("ObjectId");
			References(x => x.City)
				.Column("City_CityId");
			Table("Organization");
		}
	}
}