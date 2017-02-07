using Autofac;
using Chassis.Apps;
using Chassis.Types;

namespace Chassis.IntegrationTests.Apps
{
    public class SampleApp : IApplicationMarker
    {
        public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
        {

        }
    }
}
