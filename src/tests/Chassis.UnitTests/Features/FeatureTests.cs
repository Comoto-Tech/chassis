using Chassis.Features;
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


        class SampleFeature : Feature { }
    }
}
