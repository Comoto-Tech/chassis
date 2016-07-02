using System;
using Chassis.Types;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Types
{
    public class TypeExtensionTests
    {
        [Test]
        public void Implements()
        {
            typeof(D).Implements<IDisposable>().ShouldBe(true);
            typeof(D).Implements(typeof(IDisposable)).ShouldBe(true);
        }

        [Test]
        public void DoesntImplement()
        {
            typeof(ND).Implements<IDisposable>().ShouldBe(false);
            typeof(ND).Implements(typeof(IDisposable)).ShouldBe(false);
        }


        class ND { }
        class D : IDisposable
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}