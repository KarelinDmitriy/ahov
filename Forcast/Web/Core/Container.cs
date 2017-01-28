using AhovRepository;
using AhovRepository.Factory;
using AhovRepository.Repository;
using Ninject;

namespace Web.Core
{
	public class Container
	{
		public virtual IKernel CreateKernel()
		{
			IKernel kernel = new StandardKernel();
			kernel.Bind<IDatabaseProvider>().To<MySqlDatabaseProvider>();
			kernel.Bind<IAccessProvider>().To<FakeAccessProvider>();
			kernel.Bind<ICityProviderFactory>().To<CityProviderFactory>();
			kernel.Bind<IOrgDataproviderFactory>().To<OrgDataproviderFactory>();
			kernel.Bind<IUserCache>().To<UserCache>();
			return kernel;
		}
	}
}