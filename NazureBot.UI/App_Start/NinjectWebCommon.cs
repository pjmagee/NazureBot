using NazureBot.UI;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace NazureBot.UI
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Microsoft.WindowsAzure.ServiceRuntime;

    using NazureBot.Core.Infrastructure.EF;
    using NazureBot.Core.Services.Network;
    using NazureBot.Core.Services.User;

    using Ninject;
    using Ninject.Web.Common;

    /// <summary>
    /// The ninject web common.
    /// </summary>
    public static class NinjectWebCommon 
    {
        /// <summary>
        /// The bootstrapper
        /// </summary>
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            if (RoleEnvironment.IsEmulated)
            {
                kernel.Bind<INetworkService>().To<FakeNetworkService>().InRequestScope();
                kernel.Bind<IUserService>().To<FakeIrcUserService>().InRequestScope();
            }
            else
            {
                kernel.Bind<INetworkService>().To<NetworkService>().InRequestScope();
                kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            }
            
            kernel.Bind<DatabaseContext>().ToSelf().InRequestScope();
        }        
    }
}
