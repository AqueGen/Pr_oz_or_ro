using Autofac;
using Kapitalist.Data.Store;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Security;
using Kapitalist.Services.Prozorro.Providers;
namespace Kapitalist.Core.CompositionRoot
{
    public class ProvidersDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new AccessManager(
                context.Resolve<StoreContext>()))
                .As<IAccessManager>()
                .InstancePerRequest();

            builder.Register(context => new SchemesProvider(
                context.Resolve<StoreContext>(),
                context.Resolve<IAccessManager>()))
                .As<ISchemesProvider>()
                .InstancePerRequest();

            builder.Register(context => new TenderProvider(
                context.Resolve<StoreContext>(),
                context.Resolve<IAccessManager>()))
                .As<ITenderProvider>()
                .InstancePerRequest();

            builder.Register(context => new PlanProvider(
                context.Resolve<StoreContext>(),
                context.Resolve<IAccessManager>()))
                .As<IPlanProvider>()
                .InstancePerRequest();

            builder.Register(context => new ProfileProvider(
                context.Resolve<StoreContext>(),
                context.Resolve<IAccessManager>()))
                .As<IProfileProvider>()
                .InstancePerRequest();

            builder.Register(context => new DraftProvider(
                context.Resolve<StoreContext>(),
                context.Resolve<IAccessManager>()))
                .As<IDraftProvider>()
                .InstancePerRequest();

            builder.Register(context => new DraftPlanProvider(
                context.Resolve<StoreContext>(),
                context.Resolve<IAccessManager>()))
                .As<IDraftPlanProvider>()
                .InstancePerRequest();
        }
    }
}