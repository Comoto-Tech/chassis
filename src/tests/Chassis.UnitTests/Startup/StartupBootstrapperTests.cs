using System.Collections.Generic;
using Chassis.Startup;
using NSubstitute;
using NUnit.Framework;

namespace Chassis.UnitTests.Startup
{
    public class StartupBootstrapperTests
    {
        [Test]
        public void LightItUp()
        {
            var ss = Substitute.For<IStartupStep>();
            var sb = new StartupBootstrapper(new List<IStartupStep> {ss});

            sb.LightItUp();

            ss.Received().Execute();
        }
    }
}
