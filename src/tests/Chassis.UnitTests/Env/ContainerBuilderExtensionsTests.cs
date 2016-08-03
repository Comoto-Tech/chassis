using System;
using Autofac;
using Autofac.Core;
using Chassis.Env;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Env
{
    public class ContainerBuilderExtensionsTests
    {
        public class DoubleRegistration
        {
            [Test]
            public void NoDoubleRegistration()
            {
                AppEnv.Reset();
                var cb = new ContainerBuilder();

                Assert.Throws<Exception>(() =>
                {
                    cb.RegisterEnvironmentType<IService>(env =>
                    {
                        env.On<ProdService>(AppEnv.PRODUCTION);
                        env.On<TestService>(AppEnv.PRODUCTION);
                    });
                });
            }
        }

        public class Happy
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

            [Test]
            public void BadEnv()
            {
                AppEnv.Set("BAD");
                Assert.Throws<DependencyResolutionException>(() => { _container.Resolve<IService>(); });
            }
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
