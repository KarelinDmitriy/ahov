using System;
using System.Collections.Generic;
using System.Linq;

namespace Forcast.V2
{
	public abstract class CloudCalculator
	{
		protected readonly IEnumerable<BarrelV2> Barrels;
		protected readonly StorageData StorageData;
		protected readonly ActiveData ActiveData;

		public readonly IntermediateValues iv = new IntermediateValues();

		protected CloudCalculator(IEnumerable<BarrelV2> barrels,
			StorageData storageData,
			ActiveData activeData)
		{
			this.Barrels = barrels;
			this.StorageData = storageData;
			this.ActiveData = activeData;
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
			NpaNopa();
		}

		protected abstract void NpaNopa();

		private void Soap()
		{
			var soap = new double[5];
			var r = Math.PI*StorageData.Ro*StorageData.Ro/2;
			var d = Math.PI * (Barrels.Max(x => x.D) + Barrels.Min(x => x.D))/2;
			for (var i = 0; i < 5; i++)
			{
				var sdivg = Math.Pow(iv.Sas[i, 0]/iv.Gas[i, 0], 0.8);
				if (iv.Ton*ActiveData.U* sdivg <= r && r <= iv.Gau[i, 0]*StorageData.Ro)
				{
					soap[i] = ActiveData.T <= iv.Ton
						? ActiveData.T*ActiveData.U*sdivg + d
						: iv.Ton*sdivg + d;
				}
				else if (iv.Gas[i, 0] < iv.Ton*StorageData.Ro)
				{
					soap[i] = ActiveData.T*ActiveData.U <= iv.Gas[i, 0]
						? ActiveData.T*ActiveData.U*sdivg + d
						: sdivg + d;
				}
				else
				{
					soap[i] = ActiveData.T*ActiveData.U <= StorageData.Ro
						? ActiveData.T*ActiveData.U*sdivg + d
						: StorageData.Ro*sdivg + d;
				}
			}
			iv.Soap = soap;
		}

		private void Ton()
		{
			iv.Ton = ActiveData.To + Math.Max(StorageData.Top, StorageData.Tow);
		}

		protected abstract void SapSawApp();

		private void GauSau()
		{
			var gau = new DoubleArray();
			var sau = new DoubleArray();
			var gdl = StorageData.Gdl;
			var gl = StorageData.Gl;
			Func<double, double> f = x => gdl + Math.Min(0.3*(x - gdl), 4000) - 15*StorageData.W;
			foreach (var i in Index.GenerateIndices(5,2))
			{
				if (iv.Gas[i] <= gdl)
					gau[i] = iv.Gas[i];
				else if (gdl < iv.Gas[i] && iv.Gas[i] <= gdl + gl)
					gau[i] = f(iv.Gas[i]); 
				else if (iv.Gas[i] > gdl && gl < 4000)
					gau[i] = iv.Gas[i] >= 3.5*gl + gdl
						? iv.Gas[i] - 2.5*gl - 15*StorageData.W
						: f(iv.Gas[i]);
				else if (gl > 4000)
					gau[i] = iv.Gas[i] >= 3.5*gl + gdl
						? gdl + 4000 - 15*StorageData.W
						: f(iv.Gas[i]);
				else 
					throw new InvalidOperationException("Ќеизвестна€ ветьв выполнени€");
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
				gas[i] = Math.Max(iv.Gao1[i], iv.Gao2[i]);
				sas[i] = Math.Max(iv.Sao1[i], iv.Sao2[i]) + Math.Min(iv.Sao1[i], iv.Sao2[i])/2;
			}
			iv.Gas = gas;
			iv.Sas = sas;
		}

		private void GaoSao()
		{
			var rz = StorageData.Rz;
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
						gao1[i] = iv.Gam1[i] <= ka*StorageData.÷х + rz
							? rz + (iv.Gam1[i] - rz)/ka
							: iv.Gam1[i] - (1 - 1/ka)*StorageData.÷х;
						sao1[i] = iv.Sam1[i]*gao1[i]/iv.Gam1[i];
					}
					else
					{
						var kak = 1/ka;
						gao1[i] = StorageData.÷х >= iv.Gam1[i] - rz
							? (iv.Ga1[i] + rz)/kak - rz
							: (iv.Ga1[i] - StorageData.÷х)/kak + StorageData.÷х;
						sao1[i] = iv.Sam1[i]*gao1[i]/iv.Gam1[i];
					}
				}
				// опипаст убрать
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
						gao2[i] = iv.Gam2[i] <= ka * StorageData.÷х + rz
							? rz + (iv.Gam2[i] - rz) / ka
							: iv.Gam2[i] - (1 - 1 / ka) * StorageData.÷х;
						sao2[i] = iv.Sam2[i] * gao2[i] / iv.Gam2[i];
					}
					else
					{
						var kak = 1 / ka;
						gao2[i] = StorageData.÷х >= iv.Gam2[i] - rz
							? (iv.Ga2[i] + rz) / kak - rz
							: (iv.Ga2[i] - StorageData.÷х) / kak + StorageData.÷х;
						sao2[i] = iv.Sam2[i] * gao2[i] / iv.Gam2[i];
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
			var ku2 = ActiveData.Ku2;
			var qpa = iv.Qpa1;
			var kf = ActiveData.AirVerticalStable;
			iv.Sa2 = qpa.Select(x => ku2 * ku2 * kf.Ks * Math.Pow(x, kf.Ka));
			iv.Sam2 = qpa.Select(x => ku2 * ku2 * kf.Ksm * Math.Pow(x / kp, kf.Kam));
		}

		private void GaGam2(double kp)
		{
			var qpa = iv.Qpa2;
			var kf = ActiveData.AirVerticalStable;
			//TODO: что такое KT2 ????????????????????????????????????????????
			iv.Ga2 = qpa.Select(x => kf.Kg * ActiveData.Ku2 * Math.Pow(x, kf.Kv));
			iv.Gam2 = qpa.Select(x => kf.Kgm * ActiveData.Ku2 * Math.Pow(x / kp, kf.Kvm));
		}

		protected abstract void Qpa1(double kp);


		private void GaGam1(double kp)
		{
			var qpa = iv.Qpa1;
			var kf = ActiveData.AirVerticalStable;
			iv.Ga1 = qpa.Select(x => kf.Kg * ActiveData.Ku9 * Math.Pow(x, kf.Kv));
			iv.Gam1 = qpa.Select(x => kf.Kgm * ActiveData.Ku9 * Math.Pow(x / kp, kf.Kvm));
		}

		private void SaSam1(double kp)
		{
			var ku9 = ActiveData.Ku9;
			var qpa = iv.Qpa1;
			var kf = ActiveData.AirVerticalStable;
			iv.Sa1 = qpa.Select(x => ku9*ku9*kf.Ks*Math.Pow(x, kf.Ka));
			iv.Sam1 = qpa.Select(x => ku9*ku9*kf.Ksm*Math.Pow(x/kp, kf.Kam));
		}

		protected abstract void Qpa2(double kp);
	}
}