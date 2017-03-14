using System;
using Forcast.Matters;

namespace Forcast.V2
{
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
			? 1.22*Q2/(Math.Sqrt(H)*Math.Sqrt(Matter.Data.Density)) 
			: 5.04*Math.Sqrt(Q2/Matter.Data.Density);
	}
}