using System;
using static System.Math;

namespace Forcast.V2
{
	internal class SawSapCalculator
	{
		private readonly IntermediateValues iv;
		private readonly bool faaIsZero;
		private double T;
		private double U;
		private double Rz;
		private DoubleArray gau;
		private double Cy;
		private double q;
		private DoubleArray sau;
		private double[] tn;
		private double Cx;
		private double[] apr;
		private double[] aw;
		private double[] au;
		private double[] aa;
		private double[] b;
		private double[] bw;
		private double[] bu;
		private double[] ba;

		public SawSapCalculator(StorageData storagaData,
			ActiveData activeData,
			IntermediateValues iv, 
			bool faaIsZero)
		{
			this.iv = iv;
			this.faaIsZero = faaIsZero;
			T = activeData.T;
			U = activeData.U;
			Rz = storagaData.Rz;
			gau = iv.Gau;
			sau = iv.Sau;
			Cy = storagaData.Цу;
			Cx = storagaData.Цх;
			q = activeData.q;
			tn = activeData.Tn;
			apr = storagaData.apr;
			aw = storagaData.aw;
			au = storagaData.au;
			aa = storagaData.aa;
			b = storagaData.b;
			bw = storagaData.bw;
			bu = storagaData.bu;
			ba = storagaData.bu;
		}

		public void Calculate()
		{
			var sap = new DoubleArray();
			var saw = new DoubleArray();
			var app = new DoubleArray();
			var cyp = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				var j = i.Y;
				if (T * U <= Rz && gau[i] <= Rz)
				{
					sap[i] = saw[i] = app[i] = 0;
					continue;
				}
				var Cyp = (Cy / 2 + q) >= sau[i] / (2 * gau[i])
					? sau[i] / gau[i] - q + Cy / 2
					: Cy;
				cyp[i] = Cyp;
				var y = Cyp <= sau[i] / gau[i]
					? Cyp
					: sau[i] / gau[i];
				if (tn[i.Y] * U <= Rz && Rz <= T * U && T * U <= gau[i] && T*U <= Cy + Rz) //1 
				{
					sap[i] = 0;
					saw[i] = y * (T * U - Rz);
					var fan = FaFunc(apr[j], b[j], Rz / U - tn[j]);
					var fawn = FaFunc(aw[j], bw[j], Rz / U - tn[j]);
					var faun = FaFunc(au[j], bu[j], Rz / U - tn[j]);
					var faan = FaFunc(aa[j], ba[j], Rz / U - tn[j]);
					var fak = FaFunc(apr[j], b[j], T - tn[j]);
					var fawk = FaFunc(aw[j], bw[j], T - tn[j]);
					var fauk = FaFunc(au[j], bu[j], T - tn[j]);
					var faak = FaFunc(aa[j], ba[j], T - tn[j]);
					var fa = Avg(fan, fak);
					var faw = Avg(fawn, fawk);
					var fau = Avg(faun, fauk);
					var faa = faaIsZero ? 0 : Avg(faan, faak);
					app[i] = (1 - fa) * (1 - faw) * (1 - fau) * (1 - faa);

				}
				else if (tn[i.Y] * U <= Rz && Rz <= gau[i] && gau[i] <= T * U && gau[i] <= Cx + Rz) //2
				{
					sap[i] = 0;
					saw[i] = y * (gau[i] - Rz);
					var fan = FaFunc(apr[j], b[j], Rz / U - tn[j]);
					var fawn = FaFunc(aw[j], bw[j], Rz / U - tn[j]);
					var faun = FaFunc(au[j], bu[j], Rz / U - tn[j]);
					var faan = FaFunc(aa[j], ba[j], Rz / U - tn[j]);
					var fak = FaFunc(apr[j], b[j], gau[i] / U - tn[j]);
					var fawk = FaFunc(aw[j], bw[j], gau[i] / U - tn[j]);
					var fauk = FaFunc(au[j], bu[j], gau[i] / U - tn[j]);
					var faak = FaFunc(aa[j], ba[j], gau[i] / U - tn[j]);
					var fa = Avg(fan, fak);
					var faw = Avg(fawn, fawk);
					var fau = Avg(faun, fauk);
					var faa = faaIsZero ? 0 : Avg(faan, faak);
					app[i] = (1 - fa) * (1 - faw) * (1 - fau) * (1 - faa);
				}
				else if (tn[i.Y] * U <= Rz && Cx + Rz <= T * U && Cx + Rz <= gau[i]) //3
				{
					sap[i] = 0;
					saw[i] = y * Cx;
					var fan = FaFunc(apr[j], b[j], Rz / U - tn[j]);
					var fawn = FaFunc(aw[j], bw[j], Rz / U - tn[j]);
					var faun = FaFunc(au[j], bu[j], Rz / U - tn[j]);
					var faan = FaFunc(aa[j], ba[j], Rz / U - tn[j]);
					var fak = FaFunc(apr[j], b[j], (Cx + Rz) / U - tn[j]);
					var fawk = FaFunc(aw[j], bw[j], (Cx + Rz) / U - tn[j]);
					var fauk = FaFunc(au[j], bu[j], (Cx + Rz) / U - tn[j]);
					var faak = FaFunc(aa[j], ba[j], (Cx + Rz) / U - tn[j]);
					var fa = Avg(fan, fak);
					var faw = Avg(fawn, fawk);
					var fau = Avg(faun, fauk);
					var faa = faaIsZero ? 0 : Avg(faan, faak);
					app[i] = (1 - fa) * (1 - faw) * (1 - fau) * (1 - faa);
				}
				else if (Rz <= tn[i.Y] * U && tn[i.Y] * U <= T * U && T * U <= gau[i] && T * U <= Cx + Rz) //4
				{
					sap[i] = y * (tn[j] * U - Rz);
					saw[i] = y * (T * U - tn[j] * U);
					var fak = FaFunc(apr[j], b[j], T - tn[j]);
					var fawk = FaFunc(aw[j], bw[j], T - tn[j]);
					var fauk = FaFunc(au[j], bu[j], T - tn[j]);
					var faak = faaIsZero ? 0 : FaFunc(aa[j], ba[j], T - tn[j]);
					app[i] = (1 - fak / 2) * (1 - fawk / 2) * (1 - fauk / 2) * (1 - faak / 2);
				}
				else if (Rz <= tn[i.Y] * U && gau[i].MyEquals(tn[i.Y] * U) && gau[i] <= T * U && gau[i] <= Cx + Rz) //5
				{
					sap[i] = y * (tn[j] * U - Rz);
					saw[i] = y * (gau[i] - tn[j] * U);
					var fak = FaFunc(apr[j], b[j], T - tn[j]);
					var fawk = FaFunc(aw[j], bw[j], T - tn[j]);
					var fauk = FaFunc(au[j], bu[j], T - tn[j]);
					var faak = FaFunc(aa[j], ba[j], T - tn[j]);
					app[i] = (1 - fak / 2) * (1 - fawk / 2) * (1 - fauk / 2) * (1 - faak / 2);

				}
				else if (Rz < tn[i.Y] * U && tn[i.Y] * U <= Cx + Rz && Cx+Rz <=gau[i]) //6
				{
					sap[i] = y * (tn[j] * U - Rz);
					saw[i] = y * (Cx + Rz - tn[j] * U);
					var fak = FaFunc(apr[j], b[j], (Cx + Rz) / U - tn[j]);
					var fawk = FaFunc(aw[j], bw[j], (Cx + Rz) / U - tn[j]);
					var fauk = FaFunc(au[j], bu[j], (Cx + Rz) / U - tn[j]);
					var faak = faaIsZero ? 0 : FaFunc(aa[j], ba[j], (Cx + Rz) / U - tn[j]);
					app[i] = (1 - fak / 2) * (1 - fawk / 2) * (1 - fauk / 2) * (1 - faak / 2);
				}
				else if (Rz < T * U && T * U <= tn[i.Y] * U && T * U <= gau[i] && T * U <= Cx + Rz) //7
				{
					sap[i] = y*(T*U - Rz);
					saw[i] = 0;
					app[i] = 1;
				}
				else if (Rz < gau[i] && gau[i] <= tn[i.Y] * U && gau[i] <= T * U && gau[i] <= Cx + Rz) //8
				{
					sap[i] = y*(gau[i] - Rz);
					saw[i] = 0;
					app[i] = 1;
				}
				else
				{
					sap[i] = 0;
					saw[i] = y*Cx;
					app[i] = 1;
				}

			}
			iv.Sap = sap;
			iv.Saw = saw;
			iv.App = app;
			iv.Cyp = cyp;
		}

		private double FaFunc(double arg1, double arg2, double arg3) => arg1 * (1 - Exp(-arg2 * arg3));

		private double Avg(double p1, double p2) => (p1 + p2) / 2;
	}
}