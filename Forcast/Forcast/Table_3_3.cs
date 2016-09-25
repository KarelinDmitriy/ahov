using System.Collections.Generic;

namespace Forcast
{
	public class Table_3_3
	{
		private readonly StateType _stateType;
		private readonly Dictionary<StateType, TableCell> dict = new Dictionary<StateType, TableCell>();

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

		private void FillTable()
		{
			//TODO: заполнить таблицу
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