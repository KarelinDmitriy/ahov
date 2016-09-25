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
		public double Q { get; set; } //кол-во вещества
		public double H { get; set; } //высота поддона
		public Matter Matter { get; set; }

		public double D => Draining == Draining.Vp1
			? 1.22/Sqrt(H)*Sqrt(Q/Matter.Pg)
			: 5.04*Sqrt(Q/Matter.Pg);

		public double Er(double tcw, double u)
			=> Pow(10, -6)*Sqrt(Matter.M)*Pow(10, 2.76 - 0.019*Matter.Tck + 0.024*tcw)*(5.4 + 2.7*u);
	}

	public class Matter // вещество
	{
		private ToksiDose _toksiDose;

		public double Cv { get; set; } //удельная теплоемкость
		public double Tcg { get; set; } //температура до разрушения емкости 
		public double Tck { get; set; } //температура кипения
		public double I { get; set; } //теплота испарения
		public double Pg { get; set; } //плотность жидкости
		public double M { get; set; } //моярная масса
		public double E { get; set; } //скорость испарения

		public ToksiDose ToksiDose => _toksiDose ?? (_toksiDose = new ToksiDose(ToksiAndTempture));
		public ToksiAndTempture ToksiAndTempture { get; set; }
	}

	public class ToksiAndTempture
	{
		public double Asm { get; set; } //А для смертельных
		public double Bsm { get; set; } //B для смертельных
		public double Ap { get; set; } //A для пороговых
		public double Bp { get; set; } //B для пороговых

		public double M40pr { get; set; } //коэффициент для -40 для первичного
		public double P0pr { get; set; } //коэффициент для 0 для первичного
		public double P20pr { get; set; } //коэффициент для 20 для первичного
		public double P40pr { get; set; } //коэффициент для 40 для первичного

		public double M40sc { get; set; } //коэффициент для -40 для вторичного
		public double P0sc { get; set; } //коэффициент для 0 для вторичного
		public double P20sc { get; set; } //коэффициент для 20 для вторичного
		public double P40sc { get; set; } //коэффициент для 40 для вторичного

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
		public double Kp { get; set; } //коэффициент проникновения
		public double Ay { get; set; } //доля населения (или персонала)
	}

	public static class Expentions
	{
		public static double Child(this double d) => d*0.2;
	}
}