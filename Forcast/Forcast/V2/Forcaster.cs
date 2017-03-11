using System;
using System.Collections.Generic;
using System.Linq;
using Forcast.Matters;
using static System.Math;

namespace Forcast.V2
{
	public class Forcaster
	{
		public readonly CloudCalculator cloudCalculator;

		public Forcaster(CloudCalculator cloudCalculator)
		{
			this.cloudCalculator = cloudCalculator;
		}

		public void Run()
		{
			cloudCalculator.DoAction();
		}
	}

	public class CloudCalculator
	{
		private readonly IEnumerable<BarrelV2> barrels;
		private readonly StorageData storageData;
		private readonly ActiveData activeData;

		public readonly IntermediateValues iv = new IntermediateValues();

		public CloudCalculator(IEnumerable<BarrelV2> barrels,
			StorageData storageData,
			ActiveData activeData)
		{
			this.barrels = barrels;
			this.storageData = storageData;
			this.activeData = activeData;
		}

		public void DoAction()
		{
			//TODO: расчитать нормально
			var kp = 1;
			Qpa1(kp);
			GaGam1(kp);
			SaSam1(kp);
			Qpa2(kp);
			GaGam2(kp);
			SaSam2(kp);
			GaoSao();
			GasSas();
			GauSau();
			SapSawApp();
			Ton();
			Soap();
		}

		private void Soap()
		{
			var soap = new double[5];
			var r = PI*storageData.Ro*storageData.Ro/2;
			for (var i = 0; i < 5; i++)
			{
				if (iv.Ton*activeData.U*(iv.Sas[i, 0]/iv.Gas[i, 0]) <= r && r <= iv.Gau[i, 0])
				{
					
				}
				else if (iv.Gas[i, 0] < iv.Ton)
				{
					
				}
			}
		}

		private void Ton()
		{
			iv.Ton = activeData.To + Max(storageData.Top, storageData.Tow);
		}

		private void SapSawApp()
		{
			new SawSapCalculator(storageData, activeData, iv).Calculate();
		}

		private void GauSau()
		{
			var gau = new DoubleArray();
			var sau = new DoubleArray();
			var gdl = storageData.Gdl;
			var gl = storageData.Gl;
			Func<double, double> f = x => gdl + Min(0.3*(x - gdl), 4000) - 15*storageData.W;
			foreach (var i in Index.GenerateIndices(5,2))
			{
				if (iv.Gas[i] <= gdl)
					gau[i] = iv.Gas[i];
				else if (gdl < iv.Gas[i] && iv.Gas[i] <= gdl + gl)
					gau[i] = f(iv.Gas[i]); 
				else if (iv.Gas[i] > gdl && gl < 4000)
					gau[i] = iv.Gas[i] >= 3.5*gl + gdl
						? iv.Gas[i] - 2.5*gl - 15*storageData.W
						: f(iv.Gas[i]);
				else if (gl > 4000)
					gau[i] = iv.Gas[i] >= 3.5*gl + gdl
						? gdl + 4000 - 15*storageData.W
						: f(iv.Gas[i]);
				else 
					throw new InvalidOperationException("Неизвестная ветьв выполнения");
				sau[i] = iv.Sas[i]*gau[i]/iv.Gas[i];
			}
			iv.Gau = gau;
			iv.Sau = sau;
		}

		private void GasSas()
		{
			var gas = new DoubleArray();
			var sas = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5,2))
			{
				gas[i] = Max(iv.Gao1[i], iv.Gao2[i]);
				sas[i] = Max(iv.Sao1[i], iv.Sao2[i]) + Min(iv.Sao1[i], iv.Sao2[i])/2;
			}
			iv.Gas = gas;
			iv.Sas = sas;
		}

		private void GaoSao()
		{
			var rz = storageData.Rz;
			var gao1 = new DoubleArray();
			var gao2 = new DoubleArray();
			var sao1 = new DoubleArray();
			var sao2 = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5,2))
			{
				if (iv.Gam1[i] < rz)
				{
					gao1[i] = iv.Gam1[i];
					sao1[i] = iv.Sam1[i];
				}
				else
				{
					var ka = iv.Gam1[i]/iv.Ga1[i];
					if (ka >= 1)
					{
						gao1[i] = iv.Gam1[i] <= ka*storageData.Цх
							? rz + (iv.Gam1[i] - rz)/ka
							: iv.Gam1[i] - (1 - 1/ka)*storageData.Цх;
						sao1[i] = iv.Sam1[i]*gao1[i]/iv.Gam1[i];
					}
					else
					{
						var kak = 1/ka;
						gao1[i] = storageData.Цх >= iv.Gam1[i] - rz
							? (iv.Ga1[i] + rz)/kak - rz
							: (iv.Ga1[i] - storageData.Цх)/kak + storageData.Цх;
					}
				}
				//Копипаст убрать
				if (iv.Gam2[i] < rz)
				{
					gao2[i] = iv.Gam2[i];
					sao2[i] = iv.Sam2[i];
				}
				else
				{
					var ka = iv.Gam2[i] / iv.Ga2[i];
					if (ka >= 1)
					{
						gao2[i] = iv.Gam2[i] <= ka * storageData.Цх
							? rz + (iv.Gam2[i] - rz) / ka
							: iv.Gam2[i] - (1 - 1 / ka) * storageData.Цх;
						sao2[i] = iv.Sam2[i] * gao2[i] / iv.Gam2[i];
					}
					else
					{
						var kak = 1 / ka;
						gao2[i] = storageData.Цх >= iv.Gam2[i] - rz
							? (iv.Ga2[i] + rz) / kak - rz
							: (iv.Ga2[i] - storageData.Цх) / kak + storageData.Цх;
					}
				}
			}
			iv.Gao1 = gao1;
			iv.Gao2 = gao2;
			iv.Sao1 = sao1;
			iv.Sao2 = sao2;
		}

		private void SaSam2(double kp)
		{
			var ku2 = activeData.Ku2;
			var qpa = iv.Qpa1;
			var kf = activeData.AirVerticalStable;
			iv.Sa2 = qpa.Select(x => ku2 * ku2 * kf.Ks * Pow(x, kf.Ka));
			iv.Sam2 = qpa.Select(x => ku2 * ku2 * kf.Ksm * Pow(x / kp, kf.Kam));
		}

		private void GaGam2(double kp)
		{
			var qpa = iv.Qpa2;
			var kf = activeData.AirVerticalStable;
			//TODO: что такое KT2 ????????????????????????????????????????????
			iv.Ga2 = qpa.Select(x => kf.Kg * activeData.Ku2 * Pow(x, kf.Kv));
			iv.Gam2 = qpa.Select(x => kf.Kgm * activeData.Ku2 * Pow(x / kp, kf.Kvm));
		}

		private void Qpa1(double kp) 
		{
			// formula number 12
			var result = new DoubleArray();
			var koef = kp / Pow(activeData.U, 0.7) * storageData.Kf;
			foreach (var i in Index.GenerateIndices(5,2))
			{
				var s = barrels.Sum(x => x.Q1/(x.Matter.ToksiDosesDeaf()[i]*Constants.Kad[i.X]));
				result[i] = koef*s;
			}
			iv.Qpa1 = result;
		}


		private void GaGam1(double kp)
		{
			var qpa = iv.Qpa1;
			var kf = activeData.AirVerticalStable;
			iv.Ga1 = qpa.Select(x => kf.Kg * activeData.Ku9 * Pow(x, kf.Kv));
			iv.Gam1 = qpa.Select(x => kf.Kgm * activeData.Ku9 * Pow(x / kp, kf.Kvm));
		}

		private void SaSam1(double kp)
		{
			var ku9 = activeData.Ku9;
			var qpa = iv.Qpa1;
			var kf = activeData.AirVerticalStable;
			iv.Sa1 = qpa.Select(x => ku9*ku9*kf.Ks*Pow(x, kf.Ka));
			iv.Sam1 = qpa.Select(x => ku9*ku9*kf.Ksm*Pow(x/kp, kf.Kam));
		}

		private void Qpa2(double kp)
		{
			var result = new DoubleArray();
			var koef = 2826*1000*kp*storageData.Kf/Pow(activeData.U, 0.7);
			Func<BarrelV2, double> er =
				b => 1e-6 * Sqrt(b.Matter.Data.MoleculerMass) * Pow(10, 2.76 - 0.019 * b.Matter.Data.Temperature + 0.024 * activeData.Tcw) * (5.4 + 2.7 * activeData.U);
			foreach (var i in Index.GenerateIndices(5,2))
			{
				result[i] = koef * barrels.Sum(x => x.D * x.D * er(x) * 4 / (x.Matter.ToksiDosesPorog()[i] * Constants.Kad[i.X])  );
			}
			iv.Qpa2 = result;
		}
	}

	public class BarrelV2
	{
		public Matter Matter { get; set; }
		public double Q { get; set; }
		public MatterSaveType SaveType { get; set; }
		public Draining Draining { get; set; }
		public double H { get; set; }

		public double Q1
		{
			get
			{
				if (SaveType == MatterSaveType.Cx1)
					return 0.02*Q; // function number 1
				if (SaveType == MatterSaveType.Cx2)
				{
					var kb1 = Matter.Data.SpecificHeat*(Matter.Data.CrashTemperature - Matter.Data.Temperature)/Matter.Data.BoilingHeat;
						// function number 3
					var kb2 = 0.02* Math.Pow(20/Matter.Data.Temperature,3); // function number 4
					if (kb1 > 1)
						kb1 = 1;
					if (kb1 < 0)
						kb1 = 0;
					return Q*(kb1 + kb2); // function number 2
				}
				return Q*(20/Matter.Data.Temperature); // function number 3
			}
		}

		public double Q2 => Q - Q1;

		public double D => Draining == Draining.Vp1 
			? 1.22*Q2/(Sqrt(H)*Sqrt(Matter.Data.Density)) 
			: 5.04*Sqrt(Q2/Matter.Data.Density);
	}
}