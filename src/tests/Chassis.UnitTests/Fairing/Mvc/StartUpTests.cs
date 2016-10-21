using Autofac;
using Chassis.Apps;
using Chassis.Fairing.Mvc;
using Chassis.Types;
using NUnit.Framework;

namespace Chassis.UnitTests.Fairing.Mvc
{
    public class StartUpTests
    {
        [Test]
        public void Start()
        {
            var app = AppFactory.Build<MvcApplication>(typeof(IChassisMarker).Assembly, typeof(IChassisFairingMvcMarker).Assembly);
            app.Start();
            ChassisMvcBootstrapper.Bootstrap(app);
        }
    }

    public class MvcApplication : IApplicationMarker
    {
        public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
        {

        }
    }
}
