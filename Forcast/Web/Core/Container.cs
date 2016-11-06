using AhovRepository;
using Ninject;

namespace Web.Core
{
	public class Container
	{
		public virtual IKernel CreateKernel()
		{
			IKernel kernel = new StandardKernel();
			kernel.Bind<IAhovRepository>().To<MySqlAhovReporitory>();
			return kernel;
		}
	}
}