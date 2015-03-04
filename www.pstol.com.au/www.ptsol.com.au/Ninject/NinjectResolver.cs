using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Syntax;
using System.Web.Http.Dependencies;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject;
using Ninject.Web.Common;

namespace Ninject
{
    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }
}