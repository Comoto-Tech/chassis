using Autofac;
using Chassis.Features;
using Chassis.Types;

namespace Chassis.Fairing.Mvc
{
    public class MvcStartupFeature : Feature
    {
        public override void RegisterComponents(ContainerBuilder builder, TypePool pool)
        {
            var startupActions = pool.FindImplementorsOf<IMvcStartupStep>();

            foreach (var action in startupActions)
            {
                builder.RegisterType(action)
                    .As<IMvcStartupStep>()
                    .AsSelf();
            }
        }
    }
}
