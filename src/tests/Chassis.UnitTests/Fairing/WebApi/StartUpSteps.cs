using System.Web.Http;
using Autofac;
using Chassis.Apps;
using Chassis.Fairing.WebApi;
using Chassis.Types;
using NUnit.Framework;

namespace Chassis.UnitTests.Fairing.WebApi
{
    public class StartUpTests
    {
        [Test]
        public void Start()
        {
            var app = AppFactory.Build<WebApiApplication>(typeof(IChassisMarker).Assembly,
                typeof(IChassisFairingWebApiMarker).Assembly);
            app.Start();
            HttpConfiguration cfg = null;
            app.Bootstrap(cfg);
        }
    }

    public class WebApiApplication : IApplicationMarker
    {
        public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
        {
        }
    }
}
