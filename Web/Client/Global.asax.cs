using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Kapitalist.Core.CompositionRoot;
using Kapitalist.Web.Client.Infrastructure;
using Kapitalist.Web.Framework.Attributes;

namespace Kapitalist.Web.Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // init DI modules
            ApplicationDependencyConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LocalizationProvider.Register();
            //SyncThread.Start();
            //Database.SetInitializer<IdentityContext>(null);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomLocationViewEngine());
        }
    }
}