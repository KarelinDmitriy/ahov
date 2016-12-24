using System;
using System.Collections.Generic;
using System.Linq;
using Forcast.ForecastResults;
using static System.Math;

namespace Forcast
{
	public class Forcast
	{
		private const int powerLevel = 5;
		private const int peapleTypes = 2;

		private readonly IEnumerable<Barrel> _barrels;
		private readonly StorageData _storageData;
		private readonly ActiveData _activeData;
		private readonly Table_3_3 _airStable;

		public Forcast(IEnumerable<Barrel> barrels, StorageData storageData, ActiveData activeData)
		{
			_barrels = barrels;
			_storageData = storageData;
			_activeData = activeData;
			_airStable = activeData.AirVerticalStable;
		}

		public FullForecastResult DoForcast()
		{
			var resultForDef = ForcastForDef();
			var resultWithoutDef = ForcastForWithoutDef();
			//общее количество пораженных среди населения
			var nps = resultForDef.Npa.Select((x, i) => x + resultWithoutDef.Np[i]);
			//общее количество пораженных среди персонала
			var nos = resultForDef.Nopa.Select((x, i) => x + resultWithoutDef.Nop[i]).ToArray();
			//кол-во пораженный среди населения с учетом нормального распределения
			var nf = CalcNf(nps);
			//кол-во пораженных среди персонала с учетом нормального распреденения 
			var nof = CalcNf(new DoubleArray(nos)).ToDoubleArray();
			//кол-во пораженных без учета возроста
			var nf_all = nf.Zip((a, b) => a + b);
			//общее кол-во пораженных с учетом возрастов
			var nfs = new DoubleArray(nof, 2).Select((x, i) => x + nf[i]);
			//общее кол-во пораженных без учета возрастов
			var nfs_all = nfs.Zip((a, b) => a + b);
			//Санитарные потери
			var nfs_san = CalcNf_Sanitar(nfs);
			var nfs_all_san = nfs_san.Sum();
			//продолжительность загрезнения
			var ts = _barrels.Select(x => x.Q/(2826*x.Er(_activeData.Tcw, _activeData.U)*x.D));
			//TODO: время нахождения в противогазах (3.85)
			return new FullForecastResult
			{
				Go1 = resultWithoutDef.Go1,
				Go2 = resultWithoutDef.Go2,
				So1 = resultWithoutDef.So1,
				So2 = resultWithoutDef.So2,
				Nps = nps,
				Nf = nf,
				Nf_san = nfs_san, //TODO: Error (расчитать)
				Nos = nos,
				Nfs = nfs,
				Nfs_San = nfs_san,
				Nof = nof,
				Nof_San = nfs_all_san //TODO: ERROR (проверить)
			};
		}

		private DoubleArray CalcNf(DoubleArray nps)
		{
			var result = new DoubleArray();
			for (var i = 0; i < 1; i++)
			{
				result[0, i] = 0.6 * nps[0, i] + 0.1 * nps[1, i] + 0.05 * nps[2, i];
				result[1, i] = 0.25 * nps[0, i] + 0.5 * nps[1, i] + 0.1 * nps[2, i] + 0.05 * nps[3, i];
				result[2, i] = 0.1 * nps[0, i] + 0.25 * nps[1, i] + 0.5 * nps[2, i] + 0.1 * nps[3, i] + 0.05 * nps[4, i];
				result[3, i] = 0.05 * nps[0, i] + 0.1 * nps[1, i] + 0.25 * nps[2, i] + 0.5 * nps[3, i] + 0.1 * nps[4, i];
				result[4, i] = 0.05 * nps[1, i] + 0.1 * nps[2, i] + 0.25 * nps[3, i] + 0.5 * nps[4, i];
			}
			return result;
		}

		private double[] CalcNf_Sanitar(DoubleArray nfs)
		{
			var result = new double[2];
			result[0] = nfs[0, 0] + nfs[1, 0] + nfs[2, 0] + nfs[3, 0];
			result[1] = nfs[0, 1] + nfs[1, 1] + nfs[2, 1] + nfs[3, 1];
			return result;
		}

		//расчет для тех, что применяет антидоты
		private ForecastResultForDef ForcastForDef()
		{
			var clouds = CalcClouds();
			var d = CalcD();
			var kp = _storageData.QInside.Sum(x => x.Ay * x.Kp);
			var qpa1 = GetQpa1(kp, Constants.Kad);
			var ga1 = qpa1.Select(x => _airStable.Kg * _activeData.Ku9 * Pow(x, _airStable.Kv));
			var gam1 = qpa1.Select(x => _airStable.Kgm * _activeData.Ku9 * Pow(x / kp, _airStable.Kvm));
			var sa1 = qpa1.Select(x => Pow(_activeData.Ku9, 2) * _airStable.Ks * Pow(x, _airStable.Ka));
			var sam1 = qpa1.Select(x => Pow(_activeData.Ku9, 2) * _airStable.Ksm * Pow(x / kp, _airStable.Kam));
			var qpa2 = GetQpa2(kp, Constants.Kad);
			var ga2 = qpa2.Select(x => _airStable.Kg * _activeData.Ku2 * Pow(x, _airStable.Kv));
			var gam2 = qpa2.Select(x => _airStable.Kgm * _activeData.Ku2 * Pow(x / kp, _airStable.Kvm));
			var sa2 = qpa2.Select(x => _airStable.Ks * Pow(_activeData.Ku2, 2) * Pow(x / kp, _airStable.Ka));
			var sam2 = qpa2.Select(x => _airStable.Ksm * Pow(_activeData.Ku2, 2) * Pow(x / kp, _airStable.Kam));
			var gaoAndSao1 = CalcGaoAndSao(ga1, gam1, sa1, sam1);
			var gaoAndSao2 = CalcGaoAndSao(ga2, gam2, sa2, sam2);
			var gasAndSas = CalcGasAndSas(gaoAndSao1, gaoAndSao2);
			var gauAndSau = CalcGuaAndSau(gasAndSas.Item1, gasAndSas.Item2);
			var squareBeforeAndAfter = CalcSomthing(gauAndSau.Item1, gauAndSau.Item2);
			var ton = _activeData.To + Max(_storageData.Top, _storageData.Tow);
			var soap = CalcSoap(gasAndSas.Item1, gasAndSas.Item2, gauAndSau.Item1, ton, d); //TODO: вроде не старашно, если посчитаем для детей тоже, просто не будем выводить. 
			var npa = CalcNpa(squareBeforeAndAfter.Sap, squareBeforeAndAfter.Saw, squareBeforeAndAfter.aap, squareBeforeAndAfter.ЦyP);
			var nopa = CalcNopa(soap, _storageData.aao);
			return new ForecastResultForDef
			{
				Npa = npa,
				Nopa = nopa,
				Gao1 = gaoAndSao1.Item1,
				Gao2 = gaoAndSao2.Item1,
				Sao1 = gaoAndSao1.Item2,
				Sao2 = gaoAndSao2.Item2
			};
		}

		//расчет для тех, кто не применяет антидоты
		private ForecastResultWithoutDef ForcastForWithoutDef()
		{
			var clouds = CalcClouds();
			var d = CalcD();
			var kp = _storageData.QInside.Sum(x => x.Ay * x.Kp);
			var qp1 = GetQpa1(kp, Constants.KadForWithoutDef);
			var g1 = qp1.Select(x => _airStable.Kg * _activeData.Ku9 * Pow(x, _airStable.Kv));
			var gm1 = qp1.Select(x => _airStable.Kgm * _activeData.Ku9 * Pow(x / kp, _airStable.Kvm));
			var s1 = qp1.Select(x => Pow(_activeData.Ku9, 2) * _airStable.Ks * Pow(x, _airStable.Ka));
			var sm1 = qp1.Select(x => Pow(_activeData.Ku9, 2) * _airStable.Ksm * Pow(x / kp, _airStable.Kam));
			var qp2 = GetQpa2(kp, Constants.KadForWithoutDef);
			var g2 = qp2.Select(x => _airStable.Kg * _activeData.Ku2 * Pow(x, _airStable.Kv));
			var gm2 = qp2.Select(x => _airStable.Kgm * _activeData.Ku2 * Pow(x / kp, _airStable.Kvm));
			var s2 = qp2.Select(x => _airStable.Ks * Pow(_activeData.Ku2, 2) * Pow(x / kp, _airStable.Ka));
			var sm2 = qp2.Select(x => _airStable.Ksm * Pow(_activeData.Ku2, 2) * Pow(x / kp, _airStable.Kam));
			var goAndSo1 = CalcGaoAndSao(g1, gm1, s1, sm1);
			var goAndSo2 = CalcGaoAndSao(g2, gm2, s2, sm2);
			var gsAndSs = CalcGasAndSas(goAndSo1, goAndSo2);
			var guAndSu = CalcGuaAndSau(gsAndSs.Item1, gsAndSs.Item2);
			//тут не начинатеся гадасть
			var squareBeforeAndAfter = CalcSomthing(guAndSu.Item1, guAndSu.Item2, true);
			var ton = _activeData.To + Max(_storageData.Top, _storageData.Tow);
			var sop = CalcSoap(gsAndSs.Item1, gsAndSs.Item2, guAndSu.Item1, ton, d); //TODO: вроде не старашно, если посчитаем для детей тоже, просто не будем выводить. 
			var np = CalcNp(squareBeforeAndAfter.Sap, squareBeforeAndAfter.Saw, squareBeforeAndAfter.aap, squareBeforeAndAfter.ЦyP);
			var nop = CalcNopa(sop, 1 - _storageData.aao);
			return new ForecastResultWithoutDef
			{
				Np = np,
				Nop = nop,
				Go1 = goAndSo1.Item1,
				Go2 = goAndSo2.Item1,
				So1 = goAndSo1.Item2,
				So2 = goAndSo2.Item2
			};
		}

		private DoubleArray CalcNp(DoubleArray sp, DoubleArray sw, DoubleArray ap, DoubleArray цуР)
		{
			var result = new DoubleArray();
			foreach (var i in Index.GenerateIndices(powerLevel, peapleTypes))
			{
				result[i] = _storageData.N * _storageData.a[i.Y] * (1 - _storageData.aa[i.Y])
							* (sp[i] - sp[i.PrevX()] + ap[i] * sw[i] - ap[i.PrevX()] * sw[i.PrevX()])
							/ _storageData.Цх * цуР[i];
			}
			return result;
		}

		private double[] CalcNopa(DoubleArray soap, double aKoef)
		{
			var nopa = new double[powerLevel];
			for (var i = 0; i < powerLevel; i++)
			{
				//TODO: как считать soap[0] - soap[-1]
				nopa[i] = _storageData.No * aKoef * (soap[i, 0] - soap[i - 1, 0]) / (PI * Pow(_storageData.Ro, 2));
			}
			return nopa;
		}

		private DoubleArray CalcNpa(DoubleArray sap, DoubleArray saw, DoubleArray aap, DoubleArray цур)
		{
			var npa = new DoubleArray();
			foreach (var i in Index.GenerateIndices(powerLevel, peapleTypes))
			{
				//TODO:  как считать sap[0] - sap[-1]
				npa[i] = _storageData.N * _storageData.a[i.Y] * _storageData.aa[i.Y]
						 * (sap[i] - sap[i.PrevX()] + aap[i] * saw[i] - aap[i.PrevX()] * saw[i.PrevX()])
						 / (_storageData.Цх * цур[i]);
			}
			return npa;
		}

		private double CalcD() => _barrels.Max(x => x.D) + _barrels.Min(x => x.D);

		private DoubleArray CalcSoap(DoubleArray gas, DoubleArray sas, DoubleArray gau, double ton, double d)
		{
			var u = _activeData.U;
			var t = _activeData.T;
			var soap = new DoubleArray();
			foreach (var i in Index.GenerateIndices(powerLevel, peapleTypes))
			{
				var var1 = Pow(sas[i] / gas[i], 0.8);
				if (ton * u * var1 <= PI * Pow(_storageData.Ro, 2) / 2
					&& PI * Pow(_storageData.Ro, 2) / 2 <= _storageData.Ro
					&& PI * Pow(_storageData.Ro, 2) / 2 <= gau[i])
				{
					soap[i] = _activeData.T < ton ? _activeData.T * _activeData.U * var1 + PI * d * d / 2 : ton * var1 + PI * d * d * 2;
				}
				else if (gas[i] < ton && gas[i] < _storageData.Ro)
				{
					soap[i] = t * u < _storageData.Ro ? t * u * var1 + PI * d * d / 2 : var1 + PI * d * d / 2;
				}
				else
					soap[i] = t * u <= _storageData.Ro ? t * u * var1 + PI * d * d / 2 : _storageData.Ro * var1 + PI * d * d / 2;
			}
			return soap;
		}

		private SquareBeforeAndAfter CalcSomthing(DoubleArray gau, DoubleArray sau, bool isFaaZero = false)
		{
			var rz = _storageData.Rz;
			var q = _activeData.q;
			var u = _activeData.U;
			var t = _activeData.T;
			var sap = new DoubleArray();
			var saw = new DoubleArray();
			var aap = new DoubleArray();
			var цур = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				var tn = _activeData.Tn[i.Y];
				var ii = i.Y;
				var rzu = rz / u - tn;
				if (_activeData.T * _activeData.U <= rz || gau[i] <= rz)
					sap[i] = saw[i] = 0; // на всякий случай))) 
				var cup = _storageData.Цу / 2 + q >= sau[i] / (2 * gau[i]) ? sau[i] / gau[i] - q + _storageData.Цу / 2 : _storageData.Цу;
				var y = cup < sau[i] / gau[i] ? cup : sau[i] / gau[i];
				цур[i] = y;
				if (tn * u <= t * u && rz < t*u && t * u <= gau[i] * _storageData.Цу + rz)
				{
					sap[i] = 0;
					saw[i] = y * (t * u - rz);
					var fan = _storageData.apr[ii] * (1 - Exp(-_storageData.b[ii] * rzu));
					var fawn = _storageData.aw[ii] * (1 - Exp(-_storageData.bw[ii] * rzu));
					var faun = _storageData.au[ii] * (1 - Exp(-_storageData.bu[ii] * rzu));
					var faan = _storageData.aa[ii] * (1 - Exp(-_storageData.ba[ii] * rzu));
					double fak, fawk, fauk, faak;
					Formala347(t, out fak, out fawk, out fauk, out faak, tn, ii);
					double fa, faw, fau, faa;
					Formula348(fan, fawn, faun, faan, fak, fawk, fauk, faak, out fa, out faw, out fau, out faa);
					Formula349(aap, fa, faw, fau, isFaaZero ? 0 : faa, i);
				}
				else if (tn * u < rz && rz < gau[i] && gau[i] <= t * u && gau[i] <= _storageData.Цу + rz) //TODO: что значит gau[i] < (T*U; Цх+rz)
				{
					sap[i] = 0;
					saw[i] = y * (gau[i] - rz);
					var fan = _storageData.apr[ii] * (1 - Exp(-_storageData.b[ii] * rzu));
					var fawn = _storageData.aw[ii] * (1 - Exp(-_storageData.bw[ii] * rzu));
					var faun = _storageData.au[ii] * (1 - Exp(-_storageData.bu[ii] * rzu));
					var faan = _storageData.aa[ii] * (1 - Exp(-_storageData.ba[ii] * rzu));
					var fak = _storageData.apr[ii] * (1 - Exp(-_storageData.b[ii] * (gau[i] / u - tn)));
					var fawk = _storageData.aw[ii] * (1 - Exp(-_storageData.bw[ii] * (gau[i] / u - tn)));
					var fauk = _storageData.au[ii] * (1 - Exp(-_storageData.bu[ii] * (gau[i] / u - tn)));
					var faak = _storageData.aa[ii] * (1 - Exp(-_storageData.ba[ii] * (gau[i] / u - tn)));
					var fa = new[] { fan, fak }.Average();
					var faw = new[] { fawn, fawk }.Average();
					var fau = new[] { faun, fauk }.Average();
					var faa = isFaaZero ? 0 : new[] { faan, faak }.Average();
					aap[i] = (1 - fa) * (1 - faw) * (1 - fau) * (1 - faa);
				}
				else if (tn * u <= rz && rz + _storageData.Цу <= t * u && rz + _storageData.Цу <= gau[i])
				{
					var tmp = (_storageData.Цу + rz) / u - tn;
					sap[i] = 0;
					saw[i] = y * _storageData.Цу;
					var fan = _storageData.apr[ii] * (1 - Exp(-_storageData.b[ii] * rzu));
					var fawn = _storageData.aw[ii] * (1 - Exp(-_storageData.bw[ii] * rzu));
					var faun = _storageData.au[ii] * (1 - Exp(-_storageData.bu[ii] * rzu));
					var faan = _storageData.aa[ii] * (1 - Exp(-_storageData.ba[ii] * rzu));
					double fak, fawk, fauk, faak;
					Formula355(out fak, out fawk, out fauk, out faak, tmp, ii);
					var fa = new[] { fan, fak }.Average();
					var faw = new[] { fawn, fawk }.Average();
					var fau = new[] { faun, fauk }.Average();
					var faa = isFaaZero ? 0 : new[] { faan, faak }.Average();
					aap[i] = (1 - fa) * (1 - faw) * (1 - fau) * (1 - faa);
				}
				else if (rz < tn * u && tn * u <= t * u && t * u <= gau[i] && t * u <= _storageData.Цу + rz)
				{
					sap[i] = y * (tn * u - rz);
					saw[i] = y * (t * u - tn * u);
					double fak, fawk, fauk, faak;
					Formala347(t, out fak, out fawk, out fauk, out faak, tn, ii);
					double fa, faw, fau, faa;
					Formula348(0, 0, 0, 0, fak, fawk, fauk, faak, out fa, out faw, out fau, out faa);
					Formula349(aap, fa, faw, fau, isFaaZero ? 0 : faa, i);
				}
				else if (rz <= tn * u && gau[i].MyEquals(tn * u) && gau[i] < t * u && gau[i] < _storageData.Цу + rz)
				{
					sap[i] = y * (tn * u - rz);
					saw[i] = y * (gau[i] - tn * u);
					double fak, fawk, fauk, faak;
					Formala347(t, out fak, out fawk, out fauk, out faak, tn, ii);
					double fa, faw, fau, faa;
					Formula348(0, 0, 0, 0, fak, fawk, fauk, faak, out fa, out faw, out fau, out faa);
					Formula349(aap, fa, faw, fau, isFaaZero ? 0 : faa, i);
				}
				else if (rz < tn * u * (_storageData.Цу + rz) && tn * u * (_storageData.Цу + rz) <= t * u && tn * u * (_storageData.Цу + rz) < gau[i])
				{
					var tmp = (_storageData.Цу + rz) / u - tn;
					sap[i] = y * (tn * u - rz);
					saw[i] = y * (_storageData.Цу + rz - tn * u);
					double fak, fawk, fauk, faak;
					Formula355(out fak, out fawk, out fauk, out faak, tmp, ii);
					double fa, faw, fau, faa;
					Formula348(0, 0, 0, 0, fak, fawk, fauk, faak, out fa, out faw, out fau, out faa);
					Formula349(aap, fa, faw, fau, isFaaZero ? 0 : faa, i);
				}
				else if (rz < t * u && t * u <= tn * u && t * u <= gau[i] && t * u <= _storageData.Цу + rz)
				{
					sap[i] = y * (t * u - rz);
					saw[i] = 0;
					aap[i] = 1;
				}
				else if (rz <= gau[i] && gau[i] <= tn * u && gau[i] <= t * u && gau[i] <= _storageData.Цу + rz)
				{
					sap[i] = y * (gau[i] - rz);
					saw[i] = 0;
					aap[i] = 1;
				}
				else
				{
					sap[i] = 0;
					saw[i] = y * _storageData.Цу;
					aap[i] = 1;
				}
			}
			return new SquareBeforeAndAfter { Sap = sap, Saw = saw, aap = aap, ЦyP = цур };
		}

		private void Formula355(out double fak, out double fawk, out double fauk, out double faak, double tmp, int i)
		{
			fak = _storageData.apr[i] * (1 - Exp(-_storageData.b[i] * tmp));
			fawk = _storageData.aw[i] * (1 - Exp(-_storageData.bw[i] * tmp));
			fauk = _storageData.au[i] * (1 - Exp(-_storageData.bu[i] * tmp));
			faak = _storageData.aa[i] * (1 - Exp(-_storageData.ba[i] * tmp));
		}

		private static void Formula349(DoubleArray aap, double fa, double faw, double fau, double faa, Index i)
		{
			aap[i] = (1 - fa) * (1 - faw) * (1 - fau) * (1 - faa);
		}

		private static void Formula348(double fan, double fawn, double faun, double faan, double fak, double fawk, double fauk, double faak, out double fa, out double faw, out double fau, out double faa)
		{
			fa = new[] { fan, fak }.Average();
			faw = new[] { fawn, fawk }.Average();
			fau = new[] { faun, fauk }.Average();
			faa = new[] { faan, faak }.Average();
		}

		private void Formala347(double t, out double fak, out double fawk, out double fauk, out double faak, double tn, int ii)
		{
			fak = _storageData.apr[ii] * (1 - Exp(-_storageData.b[ii] * (t - tn)));
			fawk = _storageData.aw[ii] * (1 - Exp(-_storageData.bw[ii] * (t - tn)));
			fauk = _storageData.au[ii] * (1 - Exp(-_storageData.bu[ii] * (t - tn)));
			faak = _storageData.aa[ii] * (1 - Exp(-_storageData.ba[ii] * (t - tn)));
		}

		private Tuple<DoubleArray, DoubleArray> CalcGuaAndSau(DoubleArray gas, DoubleArray sas)
		{
			var gdl = _storageData.Gdl;
			var gl = _storageData.Gl;
			var w = _storageData.W;
			var gau = new DoubleArray();
			var sau = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				if (gas[i] <= gdl)
					gau[i] = gas[i];
				else if (gdl < gas[i] && gas[i] <= gdl + gl)
					gau[i] = gdl + Min(4000, 0.3 * (gas[i] - gdl)) - 15 * w;
				else if (gas[i] > gdl + gl && gl < 4000)
					gau[i] = gas[i] > 3.5 * gl + gdl ? gas[i] - 2.5 * gl - 15 * w : gdl + Min(4000, 0.3 * (gas[i] - gdl)) - 15 * w;
				else if (gl > 4000)
					gau[i] = gas[i] >= 3.5 * gl + gdl ? gdl + 4000 - 15 * w : gdl + Min(4000, 0.3 * (gas[i] - gdl)) - 15 * w;
				else throw new InvalidOperationException("плохая ветка выполнения");
				sau[i] = sas[i] * gau[i] / gas[i];
			}
			return new Tuple<DoubleArray, DoubleArray>(gau, sau);
		}

		private Tuple<DoubleArray, DoubleArray> CalcGasAndSas(Tuple<DoubleArray, DoubleArray> primary,
			Tuple<DoubleArray, DoubleArray> secondary)
		{
			var gas = new DoubleArray();
			var sas = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				gas[i] = Max(primary.Item1[i], secondary.Item1[i]);
				sas[i] = Max(primary.Item2[i], secondary.Item2[i]) + Min(primary.Item2[i], secondary.Item2[i]) / 2;
			}
			return new Tuple<DoubleArray, DoubleArray>(gas, sas);
		}

		private Tuple<DoubleArray, DoubleArray> CalcGaoAndSao(DoubleArray ga, DoubleArray gam, DoubleArray sa, DoubleArray sam)
		{
			var rz = _storageData.Rz;
			var gao = new DoubleArray();
			var sao = new DoubleArray();
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				if (gam[i] <= rz)
				{
					gao[i] = gam[i];
					sao[i] = sam[i];
					continue;
				}
				var ka = gam[i] / ga[i];
				if (ka >= 1)
				{
					gao[i] = gam[i] <= ka * _storageData.Цх + rz ? rz + (gam[i] - rz) / ka : gam[i] - (1 - 1 / ka) * _storageData.Цх;
					sao[i] = sam[i] * gao[i] / gam[i];
					continue;
				}
				var kak = ga[i] / gam[i];
				if (kak > 1)
				{
					gao[i] = _storageData.Цх > gam[i] - rz ? (ga[i] + rz) / kak - rz : (ga[i] - _storageData.Цх) / kak + _storageData.Цх;
					sao[i] = sa[i] * gao[i] / ga[i];
					continue;
				}
				throw new InvalidOperationException("плохая ветка выполнения");
			}
			return new Tuple<DoubleArray, DoubleArray>(gao, sao);
		}

		private QCloud[] CalcClouds()
		{
			return _barrels.Select(x =>
			{
				double primary = 0;
				switch (x.MatterSaveType)
				{
					case MatterSaveType.Cx1:
						primary = x.Q * 0.02;
						break;
					case MatterSaveType.Cx2:
						primary =
							x.Q *
							Min(x.Matter.Cv * (x.Matter.Tcg - x.Matter.Tck) / x.Matter.I, 1) //TODO: 0<value<1
							+ (x.Matter.Tck > 20 ? 0.02 * Pow((20 / x.Matter.Tck), 3) : 0.02);
						break;
					case MatterSaveType.Cx3:
						primary = x.Q * 0.02 * Min(20 / x.Matter.Tck, 20);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				return new QCloud { Barrel = x, Primary = primary, Secondary = x.Q - primary };
			}).ToArray();
		}

		private DoubleArray GetQpa1(double kp, double[] kad)
		{
			return new DoubleArray().Select((x, i) =>
			{
				return (kp / Pow(_activeData.U, .7))
					   * _storageData.Kf
					   * _barrels.Sum(y => y.Q / (y.Matter.ToksiDose.PrimaryArray[i] * kad[i.X]));
			});
		}

		public DoubleArray GetQpa2(double kp, double[] kad)
		{
			var u = _activeData.U;
			Func<Barrel, double> er =
				b =>
					Pow(10, -6) * Sqrt(b.Matter.M) * Pow(10, 2.76 - 0.019 * b.Matter.Tck + 0.024 * _activeData.Tcw) * (5.4 + 2.7 * u);
			return new DoubleArray().Select((x, i) =>
			{
				return 2826 * 1000 * kp / Pow(u, 0.7) * _storageData.Kf * _barrels.Sum(b =>
							  b.D * er(b) * 4 / (b.Matter.ToksiDose.SecondaryArray[i] * kad[i.X]));
			});
		}

		public class SquareBeforeAndAfter
		{
			public DoubleArray Sap { get; set; }
			public DoubleArray Saw { get; set; }
			public DoubleArray aap { get; set; }
			public DoubleArray ЦyP { get; set; }
		}
	}
}