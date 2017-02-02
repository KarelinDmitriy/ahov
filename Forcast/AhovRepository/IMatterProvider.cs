using System.Collections.Generic;
using System.Linq;
using Forcast.Matters;
using Newtonsoft.Json;

namespace AhovRepository
{
	public interface IMatterProvider
	{
		List<MatterData> GetAllDatas();
		MatterData GetDataByCode(string code);
	}

	public class MatterProvider : IMatterProvider
	{
		private IDatabaseProvider _databaseProvider;
		private IDictionary<string, MatterData> _localMatters;

		public MatterProvider(IDatabaseProvider databaseProvider, string josn)
		{
			_databaseProvider = databaseProvider;
			_localMatters = GetStorageDatas(josn);
		}

		public List<MatterData> GetAllDatas()
		{
			return _localMatters.Values.ToList();
		}

		public MatterData GetDataByCode(string code)
		{
			return _localMatters[code];
		}

		private IDictionary<string, MatterData> GetStorageDatas(string json)
		{
			var matters = JsonConvert.DeserializeObject<List<MatterData>>(json);
			return matters.ToDictionary(x => x.Code, x => x);
		}
	}

	public static class MatterExtensions
	{
		//public MatterData ToMatterData(this MatterEntity)
	}
}