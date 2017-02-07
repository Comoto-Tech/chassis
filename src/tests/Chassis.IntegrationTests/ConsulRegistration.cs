using Chassis.Apps;
using Chassis.IntegrationTests.Apps;
using NUnit.Framework;

namespace Chassis.IntegrationTests
{
    public class ConsulRegistration
    {
        [Test]
        public void X()
        {
            using (var app = AppFactory.Build<SampleApp>())
            {
                app.Start();
            }

        }
    }
}
