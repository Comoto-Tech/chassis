using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Multitenant;
using Chassis.Apps;
using Chassis.Meta;
using Chassis.Startup;
using Chassis.Tenants;
using Chassis.Types;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Apps
{
    public class AppFactoryTests
    {
        public class SingleTenantTests
        {
            IApplicationInstance _app;

            [OneTimeSetUp]
            public async Task SetUp()
            {
                _app = await AppFactory.Build<SingleTenant>(typeof(IChassisMarker).Assembly);
            }

            [Test]
            public void IsSingleTenant()
            {
                _app.Container.ShouldBeOfType<Container>();
                _app.Container.Resolve<ApplicationMetaData>().LoadedTenants.Count().ShouldBe(0);
            }

            [Test]
            public void IsSingleTenant_ShouldHaveApplicationInstance()
            {
                var app = _app.Resolve<IApplicationInstance>();
                app.ShouldNotBe(null);
            }

            [Test]
            public void IsSingleTenant_ShouldNotHaveTenantUnique()
            {
                TenantUnique unique;
                _app.Container.TryResolve(out unique).ShouldBe(false);
            }

            [Test]
            public void AutoScan()
            {
                Bob bob;
                _app.Container.TryResolve(out bob).ShouldBe(true);
                _app.Start();
            }


            [Test]
            public void CorrectName()
            {
                var metaData = _app.Container.Resolve<ApplicationMetaData>();
                metaData.Name.ShouldBe("SingleTenant");
            }
        }

        public class MultiTenantTests
        {
            IApplicationInstance _app;

            [OneTimeSetUp]
            public async Task SetUp()
            {
                _app = await AppFactory.Build<MultiTenant>(typeof(IChassisMarker).Assembly);
            }

            [Test]
            public void IsMultiTenant()
            {
                _app.Container.ShouldBeOfType<MultitenantContainer>();
                _app.Container.Resolve<ApplicationMetaData>().LoadedTenants.Count().ShouldBe(1);
            }


            [Test]
            public void ShouldHaveApplicationInstance()
            {
                var app = _app.Resolve<IApplicationInstance>();
                app.ShouldNotBe(null);
            }

            [Test]
            public void ShouldHaveTenantUnique()
            {
                TenantUnique unique;
                _app.Container.TryResolve(out unique).ShouldBe(true);
            }

            [Test]
            public void AutoScan()
            {
                Bob bob;
                _app.Container.TryResolve(out bob).ShouldBe(true);
                _app.Start();
            }

            [Test]
            public void CorrectName()
            {
                var metaData = _app.Container.Resolve<ApplicationMetaData>();
                metaData.Name.ShouldBe("MultiTenant");
            }
        }



        class SingleTenant : IApplicationDefinition
        {
            public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
            {

            }
        }

        public class Bob : IStartupStep
        {
            IApplicationInstance _applicationInstance;

            public Bob(IApplicationInstance applicationInstance)
            {
                _applicationInstance = applicationInstance;
            }

            public Task Execute()
            {
                return Task.FromResult(true);
            }
        }

        class MultiTenant : IApplicationDefinition
        {
            public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
            {
                builder.RegisterType<FakeStrategy>()
                    .As<ITenantIdentificationStrategy>();
            }

            class FakeStrategy : ITenantIdentificationStrategy
            {
                public bool TryIdentifyTenant(out object tenantId)
                {
                    tenantId = new TenantIdentifier("TEST");
                    return true;
                }
            }
        }

        public class TestOverrides : TenantOverrides
        {
            public override TenantIdentifier TenantId => new TenantIdentifier("TEST");

            public override void RegisterComponents(ContainerBuilder builder, TypePool pool)
            {
                builder.RegisterType<TenantUnique>()
                    .As<TenantUnique>();
            }
        }

        public class TenantUnique
        {

        }
    }
}
