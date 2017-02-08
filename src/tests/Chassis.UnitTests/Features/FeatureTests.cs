using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Chassis.Apps;
using Chassis.Features;
using Chassis.Meta;
using Chassis.Types;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Features
{
    public class FeatureTests
    {
        [Test]
        public void Name()
        {
            var sf = new SampleFeature();
            sf.Name.ShouldBe("Sample");
        }

        [Test]
        public void ToStringFormat()
        {
            var sf = new SampleFeature();
            sf.ToString().ShouldBe("Sample");
        }

        [Test]
        public async Task ShouldRegisterFeature()
        {
            using (var app = await AppFactory.Build<SampleApp>())
            {
                app.Container.Resolve<ApplicationMetaData>().LoadedFeatures.Count().ShouldBe(1);
            }
        }

        class SampleApp : IApplicationDefinition
        {
            public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
            {

            }
        }

        class SampleFeature : Feature { }
    }
}
