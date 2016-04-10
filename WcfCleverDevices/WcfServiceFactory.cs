using Microsoft.Practices.Unity;
using Unity.Wcf;
using WcfService3.DAL;

namespace WcfCleverDevices
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            // register all your components with the container here
            // container
            //    .RegisterType<IService1, Service1>()
            //    .RegisterType<DataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IService1, Service1>();
            container.RegisterType<ISqlConnection, SqlConnection>();
        }
    }    
}