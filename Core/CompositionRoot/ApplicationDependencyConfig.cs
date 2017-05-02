using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kapitalist.Core.CompositionRoot
{
    public class ApplicationDependencyConfig
    {
        /// <summary>
		/// Registers dependencies into Autofac container.
		/// </summary>
		public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // register system modules
            builder.RegisterModule(new CommonDependencyModule());
            builder.RegisterModule(new ProvidersDependencyModule());

            // register calling assembly (mvc)
            builder.RegisterControllers(Assembly.GetCallingAssembly());

            // set autofac resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}
