using System;
using static System.Math;

namespace Forcast
{
	public class QCloud //кол-во переводимое в облока
	{
		public Barrel Barrel { get; set; }
		public double Primary { get; set; } //первичное
		public double Secondary { get; set; } //вторичное
	}

	public class Barrel //бочка с веществом
	{
		public MatterSaveType MatterSaveType { get; set; }
		public Draining Draining { get; set; }
		public double Q { get; set; } //кол-во вещества (в граммах!)
		public double H { get; set; } //высота поддона
		public HMatter HMatter { get; set; }

		public double D => Draining == Draining.Vp1
			? 1.22/Sqrt(H)*Sqrt(Q/HMatter.Pg)
			: 5.04*Sqrt(Q/HMatter.Pg);

		public double Er(double tcw, double u)
			=> Pow(10, -6)*Sqrt(HMatter.M)*Pow(10, 2.76 - 0.019*HMatter.Tck + 0.024*tcw)*(5.4 + 2.7*u);
	}

	public class HMatter // вещество
	{
		private ToksiDose _toksiDose;

		/// <summary>
		/// удельная теплоемкость
		/// </summary>
		public double Cv { get; set; }
		/// <summary>
		/// температура до разрушения емкости 
		/// </summary>
		public double Tcg { get; set; }
		/// <summary>
		/// температура кипения
		/// </summary>
		public double Tck { get; set; }
		/// <summary>
		/// теплота испарения
		/// </summary>
		public double I { get; set; }
		/// <summary>
		/// плотность жидкости
		/// </summary>
		public double Pg { get; set; }
		/// <summary>
		/// моярная масса
		/// </summary>
		public double M { get; set; }
		/// <summary>
		/// скорость испарения
		/// </summary>
		public double E { get; set; } 

		public ToksiDose ToksiDose => _toksiDose ?? (_toksiDose = new ToksiDose(ToksiAndTempture));
		public ToksiAndTempture ToksiAndTempture { get; set; }
	}

	public class ToksiAndTempture
	{
		/// <summary>
		/// А для смертельных
		/// </summary>
		public double Asm { get; set; }
		/// <summary>
		/// B для смертельных
		/// </summary>
		public double Bsm { get; set; }
		/// <summary>
		/// A для пороговых
		/// </summary>
		public double Ap { get; set; }
		/// <summary>
		/// B для пороговых 
		/// </summary>
		public double Bp { get; set; }

		/// <summary>
		/// коэффициент для -40 для первичного
		/// </summary>
		public double M40pr { get; set; }
		/// <summary>
		/// коэффициент для 0 для первичного 
		/// </summary>
		public double P0pr { get; set; } 
		/// <summary>
		/// коэффициент для 20 для первичного
		/// </summary>
		public double P20pr { get; set; }
		/// <summary>
		/// коэффициент для 40 для первичного
		/// </summary>
		public double P40pr { get; set; }

		/// <summary>
		///коэффициент для -40 для вторичного
		/// </summary>
		public double M40sc { get; set; }
		/// <summary>
		///коэффициент для 0 для вторичного
		/// </summary>
		public double P0sc { get; set; }
		/// <summary>
		///коэффициент для 20 для вторичного
		/// </summary>
		public double P20sc { get; set; }
		/// <summary>
		///коэффициент для 40 для вторичного
		/// </summary>
		public double P40sc { get; set; } 

		public double Ku9Pr(double T)
		{
			if (T < 20)
				return M40pr;
			if (T >= 20 && T < 10)
				return P0pr;
			if (T >= 10 && T < 30)
				return P20pr;
			if (T >= 30)
				return P40pr;
			throw new InvalidOperationException("ошибка с double?");
		}

		public double Ku9Sc(double T)
		{
			if (T < 20)
				return M40sc;
			if (T >= 20 && T < 10)
				return P0sc;
			if (T >= 10 && T < 30)
				return P20sc;
			if (T >= 30)
				return P40sc;
			throw new InvalidOperationException("ошибка с double?");
		}
	}

	public enum MatterSaveType //тип хранения
	{
		Cx1, //изотермический
		Cx2, //под давлением
		Cx3 //при обычном хранении
	}

	public enum Draining //тип вылева жидкости
	{
		Vp1, //вылев в поддон 
		Vp2 //на ровную поверность
	}

	public class QInside //коэффциент проникновения 
	{
		/// <summary>
		/// коэффициент проникновения
		/// </summary>
		public double Kp { get; set; }
		/// <summary>
		/// доля населения (или персонала)
		/// </summary>
		public double Ay { get; set; } 
	}

	public static class Expentions
	{
		public static double Child(this double d) => d*0.2;
	}
}