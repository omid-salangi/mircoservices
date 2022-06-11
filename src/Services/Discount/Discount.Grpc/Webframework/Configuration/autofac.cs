using Autofac;
using Autofac.Extensions.DependencyInjection;
using Discount.Grpc.Common;
using Discount.Grpc.Common.Dependency;

namespace Discount.Grpc.Webframework.Configuration
{
    public static class AutofacExtention
    {
        public static void DependencyContainerWithAutofac(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutoFacModule());
            });
        }
    }

    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var pro = typeof(SiteSettings).Assembly;

            builder.RegisterAssemblyTypes(pro).AssignableTo<IScopedDependency>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(pro).AssignableTo<ITransientDependency>().AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterAssemblyTypes(pro).AssignableTo<IsingleDependency>().AsImplementedInterfaces()
                .SingleInstance();
            base.Load(builder);
        }
    }
}
