using System.Collections.Generic;
using System.Linq;
using AhovRepository;
using Forcast.Matters;
using Newtonsoft.Json;

namespace Web.Core
{
	public interface IMatterProvider
	{
		List<MatterData> GetAllDatas();
		MatterData GetDataByCode(string code);
	}

	public class MatterProvider : IMatterProvider
	{
		private IDatabaseProvider databaseProvider;
		private IDictionary<string, MatterData> localMatters;

		public MatterProvider(IDatabaseProvider databaseProvider, string josn)
		{
			this.databaseProvider = databaseProvider;
			localMatters = GetStorageDatas(josn);
		}

		public List<MatterData> GetAllDatas()
		{
			throw new System.NotImplementedException();
		}

		public MatterData GetDataByCode(string code)
		{
			throw new System.NotImplementedException();
		}

		private IDictionary<string, MatterData> GetStorageDatas(string json)
		{
			var matters = JsonConvert.DeserializeObject<List<MatterData>>(json);
			return matters.ToDictionary(x => x.Code, x => x);
		}
	}
}