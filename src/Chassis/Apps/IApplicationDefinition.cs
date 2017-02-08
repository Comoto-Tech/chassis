using Autofac;
using Chassis.Types;

namespace Chassis.Apps
{
    public interface IApplicationDefinition
    {
        void ConfigureContainer(TypePool pool, ContainerBuilder builder);
    }
}
