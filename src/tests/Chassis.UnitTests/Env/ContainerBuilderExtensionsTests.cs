using Autofac;
using Chassis.Env;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Env
{
    public class ContainerBuilderExtensionsTests
    {
        IContainer _container;

        [SetUp]
        public void SetUp()
        {
            AppEnv.Reset();
            var cb = new ContainerBuilder();

            cb.RegisterEnvironmentType<IService>(env =>
            {
                env.On<ProdService>(AppEnv.PRODUCTION);
                env.On<TestService>(AppEnv.TEST);
            });

            _container = cb.Build();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        [Test]
        public void ProdSwitch()
        {
            AppEnv.Set(AppEnv.PRODUCTION);
            var a = _container.Resolve<IService>();
            a.ShouldBeOfType<ProdService>();
        }

        [Test]
        public void TestSwitch()
        {
            AppEnv.Set(AppEnv.TEST);
            var a = _container.Resolve<IService>();
            a.ShouldBeOfType<TestService>();
        }

        interface IService
        {

        }

        class ProdService : IService
        {

        }

        class TestService : IService
        {

        }
    }
}
