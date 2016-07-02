using Autofac;
using Chassis.Types;

namespace Chassis.Apps
{
    public interface IApplicationMarker
    {
        void ConfigureContainer(TypePool pool, ContainerBuilder builder);
    }
}
