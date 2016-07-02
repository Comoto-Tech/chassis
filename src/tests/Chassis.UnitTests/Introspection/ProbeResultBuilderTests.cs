using System;
using Chassis.Introspection;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Introspection
{
    public class ProbeResultBuilderTests
    {
        [Test]
        public void SampleTests()
        {
            var sps = new SampleProbeSite();
            var result = sps.GetProbeResult();

            result.ResultId.ShouldNotBe(Guid.Empty);
            result.ProbeId.ShouldNotBe(Guid.Empty);
            result.StartTimestamp.ShouldBeLessThan(DateTime.UtcNow);
            result.Duration.ShouldBeLessThan(new TimeSpan(0,0,1,0));
            result.Host.Assembly.ShouldBe("Chassis");
            result.Results["a"].ShouldBe("b");
        }

        class SampleProbeSite : IProbeSite
        {
            public void Probe(IProbeContext context)
            {
                context.Add("a","b");
            }
        }
    }
}
