using System.Collections.Generic;
using AhovRepository.Entity;
using Forcast.Matters;

namespace Web.Models.Barrel
{
	public class BarrelModel
	{
		public BarrelEntity Barrel { get; set; }
		public List<MatterData> AvailableMatter { get; set; }
	}
}