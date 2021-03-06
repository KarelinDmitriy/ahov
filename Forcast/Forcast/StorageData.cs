﻿namespace Forcast
{
	public class StorageData
	{
		/// <summary>
		/// Сила проникновения в здания
		/// </summary>
		public QInside[] QInside { get; set; }
		/// <summary>
		/// Коеф.физической нагрузки
		/// </summary>
		public double Kf { get; set; }

		/// <summary>
		/// Радиус хим.объекта
		/// </summary>
		public double Ro { get; set; }

		/// <summary>
		/// Радиус санитарной зоны
		/// </summary>
		public double Rz { get; set; }
		
		/// <summary>
		/// Длинна города
		/// </summary>
		public double Цх { get; set; }

		/// <summary>
		/// Ширина города
		/// </summary>
		public double Цу { get; set; }

		/// <summary>
		/// Численность населения
		/// </summary>
		public double N { get; set; }

		/// <summary>
		/// Численность персонала
		/// </summary>
		public double No { get; set; }

		/// <summary>
		/// Время надевания СИЗ персоналом
		/// </summary>
		public double Top { get; set; }

		/// <summary>
		/// Время надевания СИЗ мирным населением ???
		/// </summary>
		public double Tow { get; set; }

		/// <summary>
		/// Рассояние от очага аварии до леса
		/// </summary>
		public double Gdl { get; set; }

		/// <summary>
		/// Грубина леса по направлению ветра
		/// </summary>
		public double Gl { get; set; }

		/// <summary>
		/// Сумарная высота возвышенностей
		/// </summary>
		public double W { get; set; }

		/// <summary>
		/// Доли населения применяющих СИЗ
		/// </summary>
		public double[] apr { get; set; }

		/// <summary>
		/// Доля населения занимающих укрытия
		/// </summary>
		public double[] au { get; set; }

		/// <summary>
		/// Доля населения выходящих с зараженной местности
		/// </summary>
		public double[] aw { get; set; }

		/// <summary>
		/// Доля населения применяющих антидоты
		/// </summary>
		public double[] aa { get; set; }

		/// <summary>
		/// Численность персонала, применяющих антидоты
		/// </summary>
		public double aao { get; set; }

		/// <summary>
		/// Для населения по возрастам
		/// </summary>
		public double[] a { get; set; }

		/// <summary>
		/// Параметр функции скорости для использующих противогаз
		/// </summary>
		public double[] b { get; set; }
		/// <summary>
		/// Параметр функции скорости для выходящих из зоны поражения
		/// </summary>
		public double[] bw { get; set; }
		/// <summary>
		/// Параметр функции скорости для использующих укрытия
		/// </summary>
		public double[] bu { get; set; }
		/// <summary>
		/// Параметр функции скорости для использующих антидоты
		/// </summary>
		public double[] ba { get; set; }

		/// <summary>
		/// Длинна химобъекта
		/// </summary>
		public double Цx_p { get; set; }
		/// <summary>
		/// Ширина химобъека
		/// </summary>
		public double Цу_p { get; set; }
	}
}