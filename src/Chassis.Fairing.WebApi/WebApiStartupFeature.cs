using Autofac;
using Chassis.Features;
using Chassis.Types;

namespace Chassis.Fairing.WebApi
{
    public class WebApiStartupFeature : Feature
    {
        public override void RegisterComponents(ContainerBuilder builder, TypePool pool)
        {
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
