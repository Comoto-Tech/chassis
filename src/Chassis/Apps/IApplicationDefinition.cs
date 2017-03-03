using Autofac;
using Chassis.Types;

namespace Chassis.Apps
{
    public interface IApplicationDefinition
    {
        void ConfigureApplication(TypePool pool, ContainerBuilder builder);
    }
}
