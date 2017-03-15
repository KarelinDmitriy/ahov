using Forcast.V2;

namespace Web.Models.Forcast
{
	public class ResultModel
	{
		public IntermediateValues ForDef { get; set; }
		public IntermediateValues WithoutDef { get; set; }
		public Result Result { get; set; }
	}
}