using System.Collections.Generic;
using System.Linq;

namespace AhovRepository
{
	public partial class MySqlAhovReporitory
	{
		public List<WebOrganization> GetOrganizations(int userId)
		{
			var orgs = _connection.organization.Select(x => new WebOrganization
			{
				Id = x.OrgId,
				Name = x.OgranizationName,
				PersonalCount = x.PersonalCount,
				Ro = x.Ro,
				CityId = x.City_CityId
			}).ToList();

			return orgs;
		}

		public WebOrganization GetOrganization(int orgId, int userId)
		{
			var org = _connection.organization.FirstOrDefault(x => x.OrgId == orgId);
			if (org == null)
				return null;
			return new WebOrganization
			{
				Id = org.OrgId,
				Name = org.OgranizationName,
				Aao = org.Aao,
				Ba = org.Ba,
				Ba_ch = org.Ba_ch,
				Bu = org.Bu,
				Bu_ch = org.Bu_ch,
				Bw = org.Bw,
				Bw_ch = org.Bw_ch,
				CityId = org.City_CityId,
				Gdl = org.Gdl,
				Gl = org.Gl,
				Kf = org.Kf,
				PersonalCount = org.PersonalCount,
				Ro = org.Ro,
				Rz = org.Rz,
				Top = org.Top,
				Tow = org.Tow,
				W = org.W
			};
		}

		public void AddOrganization(WebOrganization org, int userId)
		{
			_connection.organization.Add(new organization
			{
				OrgId = org.Id,
				Tow = org.Tow,
				Bu_ch = org.Bu_ch,
				Ba = org.Ba,
				Gl = org.Gl,
				Bu = org.Bu,
				Bw_ch = org.Bw_ch,
				Bw = org.Bw,
				W = org.W,
				City_CityId = org.CityId,
				Kf = org.Kf,
				Rz = org.Rz,
				Ro = org.Ro,
				Top = org.Top,
				PersonalCount = org.PersonalCount,
				Gdl = org.Gdl,
				Aao = org.Aao,
				Ba_ch = org.Ba_ch,
				OgranizationName = org.Name,
			});

			_connection.SaveChanges();
		}

		public void UpdateOrganizatino(WebOrganization org, int userId)
		{
			var baseOrg = _connection.organization.FirstOrDefault(x => x.OrgId == org.Id);
			if (baseOrg == null)
				return;

			baseOrg.Tow = org.Tow;
			baseOrg.Bu_ch = org.Bu_ch;
			baseOrg.Ba = org.Ba;
			baseOrg.Gl = org.Gl;
			baseOrg.Bu = org.Bu;
			baseOrg.Bw_ch = org.Bw_ch;
			baseOrg.Bw = org.Bw;
			baseOrg.W = org.W;
			baseOrg.City_CityId = org.CityId;
			baseOrg.Kf = org.Kf;
			baseOrg.Rz = org.Rz;
			baseOrg.Ro = org.Ro;
			baseOrg.Top = org.Top;
			baseOrg.PersonalCount = org.PersonalCount;
			baseOrg.Gdl = org.Gdl;
			baseOrg.Aao = org.Aao;
			baseOrg.Ba_ch = org.Ba_ch;
			baseOrg.OgranizationName = org.Name;
			_connection.SaveChanges();
		}
	}

}