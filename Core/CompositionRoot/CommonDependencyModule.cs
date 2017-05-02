using Autofac;
using Kapitalist.Data.Store;
using Kapitalist.Web.Security;
using Kapitalist.Services.Prozorro.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.CompositionRoot
{
    public class CommonDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new StoreContext())
                .AsSelf()
                .InstancePerRequest();
        }
    }
}
