using System.Collections.Generic;

namespace Forcast
{
	public class Table_3_3
	{
		private readonly StateType _stateType;
		private readonly Dictionary<StateType, TableCell> dict = FillTable();

		public Table_3_3(StateType stateType)
		{
			_stateType = stateType;
		}

		public double WildSpeed => dict[_stateType].WildSpeed;
		public double Kg => dict[_stateType].Kg;
		public double Kgm => dict[_stateType].Kgm;
		public double Ks => dict[_stateType].Ks;
		public double Ksm => dict[_stateType].Ksm;
		public double Kv => dict[_stateType].Kv;
		public double Kvm => dict[_stateType].Kvm;
		public double Ka => dict[_stateType].Ka;
		public double Kam => dict[_stateType].Kam;

		private static Dictionary<StateType, TableCell> FillTable()
		{
			var dict = new Dictionary<StateType, TableCell>
			{
				{
					StateType.A, new TableCell
					{
						Ka = 0.76,
						Kam = 0.84,
						Kg = 10.956,
						Kgm = 9.37,
						Ks = 146.68,
						Ksm = 32.53,
						Kv = 0.44,
						Kvm = 0.42,
						WildSpeed = 1
					}
				},
				{
					StateType.B, new TableCell
					{
						Ksm = 3.63,
						WildSpeed = 2,
						Kg = 3.16,
						Kvm = 0.5,
						Kv = 0.54,
						Ks = 16.64,
						Kam = 1,
						Kgm = 3.9,
						Ka = 0.91
					}
				},
				{
					StateType.C, new TableCell
					{
						WildSpeed = 5,
						Kg = 2.4,
						Kvm = 0.53,
						Kv = 0.59,
						Ks = 10,
						Kam = 1.05,
						Ka = 1,
						Kgm = 4.57,
						Ksm = 3.18,
					}
				},
				{
					StateType.D, new TableCell
					{
						Kg = 1.69,
						Kvm = 0.53,
						Kv = 0.59,
						Ks = 4.83,
						Kam = 1.09,
						Kgm = 5.61,
						Ksm = 4.26,
						WildSpeed = 5,
						Ka = 1.07,
					}
				},
				{
					StateType.E, new TableCell
					{
						Kvm = 0.57,
						Kv = 0.62,
						Ks = 2.8,
						Kam = 1.1,
						Kgm = 5.61,
						Ksm = 4.42,
						WildSpeed = 3,
						Kg = 1.3,
						Ka = 1.07
					}
				},
				{
					StateType.F, new TableCell
					{
						Kv = 0.66,
						Kvm = 0.6,
						Ks = 0.84,
						Ksm = 5.04,
						Kam = 1.17,
						Kgm = 6.37,
						WildSpeed = 1,
						Kg = 0.88,
						Ka = 1.24
					}
				}
			};
			return dict;
		}

		private class TableCell
		{
			public double WildSpeed { get; set; }
			public double Kg { get; set; }
			public double Kgm { get; set; }
			public double Ks { get; set; }
			public double Ksm { get; set; }
			public double Kv { get; set; }
			public double Kvm { get; set; }
			public double Ka { get; set; }
			public double Kam { get; set; }
		}
	}

	public enum StateType
	{
		A,
		B,
		C,
		D,
		F,
		E
	}
}