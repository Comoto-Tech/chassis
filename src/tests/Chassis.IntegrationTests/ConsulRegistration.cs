using System.Threading.Tasks;
using Chassis.Apps;
using Chassis.IntegrationTests.Apps;
using NUnit.Framework;

namespace Chassis.IntegrationTests
{
    public class ConsulRegistration
    {
        [Test]
        public async Task X()
        {
            using (var app = await AppFactory.Build<SampleApp>(opt => opt.GetInstanceNameFromEnvVar()))
            {
                app.Start();
            }

        }
    }
}
