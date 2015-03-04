[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(www.ptsol.com.au.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(www.ptsol.com.au.App_Start.NinjectWebCommon), "Stop")]

namespace www.ptsol.com.au.App_Start
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using System.Web.Http;
    using Ninject.Web.Common;
    using www.ptsol.com.au.DAL;
    using www.ptsol.com.au.DAL.Interfaces;
    using www.ptsol.com.au.Business;
    using www.ptsol.com.au.Business.Interfaces;
  

    public static class NinjectWebCommon 
    {
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
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
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
            kernel.Bind<IDataContext>().To<DataContext>();
            kernel.Bind<ILibraries>().To<Libraries>();
            kernel.Bind<ICaptchaService>().To<CaptchaService>();
            kernel.Bind<ICaptchaLibrary>().To<CaptchaLibrary>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserLibrary>().To<UserLibrary>();

        }        
    }
}
