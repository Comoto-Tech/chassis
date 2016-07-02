using System;
using Chassis.Introspection;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Introspection
{
    public class HostMetadataCacheTests
    {
        [Test]
        public void X()
        {
            var a = HostMetadataCache.Host;
            var b = HostMetadataCache.Host;

            Object.ReferenceEquals(a, b).ShouldBe(true);
        }
    }
}
