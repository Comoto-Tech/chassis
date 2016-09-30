using Autofac;
using Autofac.Integration.WebApi;
using Chassis.Features;
using Chassis.Types;

namespace Chassis.Fairing.WebApi
{
    public class WebApiStartupFeature : Feature
    {
        public override void RegisterComponents(ContainerBuilder builder, TypePool pool)
        {
            builder.RegisterApiControllers(pool.Assemblies);

            var startUpActions = pool.FindImplementorsOf<IWebApiStartupStep>();

            foreach (var action in startUpActions)
            {
                builder.RegisterType(action)
                    .As<IWebApiStartupStep>()
                    .AsSelf();
            }
        }
    }
}
