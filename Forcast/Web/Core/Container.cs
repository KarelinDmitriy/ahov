using AhovRepository;
using AhovRepository.Factory;
using AhovRepository.Repository;
using Ninject;

namespace Web.Core
{
	// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
	public class Container
	{
		public virtual IKernel CreateKernel(string json)
		{
			IKernel kernel = new StandardKernel();
			kernel.Bind<IDatabaseProvider>().To<MySqlDatabaseProvider>();
			kernel.Bind<IAccessProvider>().To<AccessProvider>();
			kernel.Bind<ICityProviderFactory>().To<CityProviderFactory>();
			kernel.Bind<IOrgDataproviderFactory>().To<OrgDataproviderFactory>();
			kernel.Bind<IBarrelProviderFactory>().To<BarrelProviderFactory>();
			kernel.Bind<IUserCache>().To<UserCache>();
			kernel.Bind<IMatterProvider>()
				.ToConstructor(x => new MatterProvider(x.Inject<IDatabaseProvider>(), json));
			return kernel;
		}
	}
}