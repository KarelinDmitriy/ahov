using System.Collections.Generic;
using Forcast.V2;

namespace Forcast.Report
{
	public class ReportModel
	{
		public DoubleArray Gau { get; set; }
		public DoubleArray Sau { get; set; }
		public DoubleArray Nfs { get; set; }
		public DoubleArray NfAll { get; set; }
		public InputData InputData { get; set; }
	}

	public class InputData
	{
		public ActiveData ActiveData { get; set; }
		public StorageData StorageData { get; set; }
		public List<BarrelV2> Barrels { get; set; }
	}
}