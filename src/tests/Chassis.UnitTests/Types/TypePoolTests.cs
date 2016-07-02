using Chassis.Types;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Types
{
    public class TypePoolTests
    {
        TypePool _pool;

        [SetUp]
        public void SetUp()
        {
            _pool = new TypePool(typeof(IChassisMarker).Assembly);
        }

        [Test]
        public void X()
        {
            _pool.Assemblies.Length.ShouldBe(1);
        }

        [Test]
        public void AddDuplicateTypeIsSafe()
        {
            _pool.ExplicitTypes.ShouldBeEmpty();
            _pool.AddType(typeof(TypePool));
            _pool.ExplicitTypes.ShouldContain(typeof(TypePool));
            _pool.AddType(typeof(TypePool));
            _pool.ExplicitTypes.ShouldContain(typeof(TypePool));
            _pool.ExplicitTypes.Count.ShouldBe(1);
        }

        [Test]
        public void Query()
        {
            _pool.Query().ShouldContain(typeof(TypePool));
        }
    }
}
