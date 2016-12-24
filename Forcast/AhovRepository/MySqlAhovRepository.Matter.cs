using System.Collections.Generic;
using System.Linq;

namespace AhovRepository
{
	public partial class MySqlAhovReporitory
	{
		public List<WebMatter> GetAllMatter()
		{
			return _connection.matter.Select(x => new WebMatter
			{
				Id = x.MatterId,
				Name = x.Name
			}).ToList();
		}

		public WebMatter Getmatter(int id)
		{
			var matter = _connection.matter.FirstOrDefault(x => x.MatterId == id);
			if (matter == null)
				return null;

			return new WebMatter
			{
				Id = matter.MatterId,
				Name = matter.Name,
				Bp = matter.Bp,
				Asm = matter.Asm,
				Ap = matter.Ap,
				Bsm = matter.Bsm,
				Cv = matter.Cv,
				E = matter.E,
				I = matter.I,
				M = matter.M,
				Pg = matter.Pg,
				Tcg = matter.Tcg,
				Tck = matter.Tck
			};
		}

		public void UpdateMatter(WebMatter matter)
		{
			var baseMatter = _connection.matter.FirstOrDefault(x => x.MatterId == matter.Id);
			if (baseMatter == null)
				return;


			baseMatter.MatterId = matter.Id;
			baseMatter.Name = matter.Name;
			baseMatter.Bp = matter.Bp;
			baseMatter.Asm = matter.Asm;
			baseMatter.Ap = matter.Ap;
			baseMatter.Bsm = matter.Bsm;
			baseMatter.Cv = matter.Cv;
			baseMatter.E = matter.E;
			baseMatter.I = matter.I;
			baseMatter.M = matter.M;
			baseMatter.Pg = matter.Pg;
			baseMatter.Tcg = matter.Tcg;
			baseMatter.Tck = matter.Tck;
			_connection.SaveChanges();
		}

		public void AddMatter(WebMatter matter)
		{
			_connection.matter.Add(new matter
			{
				MatterId = matter.Id,
				Name = matter.Name,
				Bp = matter.Bp,
				Asm = matter.Asm,
				Ap = matter.Ap,
				Bsm = matter.Bsm,
				Cv = matter.Cv,
				E = matter.E,
				I = matter.I,
				M = matter.M,
				Pg = matter.Pg,
				Tcg = matter.Tcg,
				Tck = matter.Tck,
			});

			_connection.SaveChanges();
		}
	}
}