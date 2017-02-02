using System.Collections.Generic;
using AhovRepository.Entity;

namespace AhovRepository.Repository
{
	public interface IBarrelProvider
	{
		List<BarrelEntity> GetBarrels(int orgId);
		BarrelEntity GetBarrel(int barrelId);
		BarrelEntity AddBarrel(BarrelEntity barrel);
		void UpdateBarrel(BarrelEntity barrel);
	}

	public class BarrelProvider : IBarrelProvider
	{
		private readonly IDatabaseProvider _databaseProvider;
		private readonly IAccessProvider _accessProvider;
		private readonly int _userId;

		public BarrelProvider(IDatabaseProvider databaseProvider,
			IAccessProvider accessProvider,
			int userId)
		{
			_databaseProvider = databaseProvider;
			_accessProvider = accessProvider;
			_userId = userId;
		}

		public List<BarrelEntity> GetBarrels(int orgId)
		{
			return _databaseProvider.Where<BarrelEntity>(x => x.Org.Id == orgId);
		}

		public BarrelEntity GetBarrel(int barrelId)
		{
			return _databaseProvider.GetOne<BarrelEntity>(x => x.BarrelId == barrelId);
		}

		public BarrelEntity AddBarrel(BarrelEntity barrel)
		{
			return _databaseProvider.Insert(barrel);
		}

		public void UpdateBarrel(BarrelEntity barrel)
		{
			_databaseProvider.Update(barrel);
		}
	}
}