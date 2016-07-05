using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extras.Multitenant;
using Chassis.Apps;
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
            IApplication _app;

            [OneTimeSetUp]
            public void SetUp()
            {
                _app = AppFactory.Build<SingleTenant>(typeof(IChassisMarker).Assembly);
            }

            [Test]
            public void IsSingleTenant()
            {
                _app.Container.ShouldBeOfType<Container>();
                _app.LoadedTenants.Count().ShouldBe(0);
            }

            [Test]
            public void IsSingleTenant_ShouldHaveApplicationInstance()
            {
                var app = _app.Resolve<IApplication>();
                app.ShouldNotBe(null);
            }

            [Test]
            public void IsSingleTenant_ShouldNotHaveTenantUnique()
            {
                TenantUnique unique;
                _app.Container.TryResolve(out unique).ShouldBe(false);
            }

        }

        public class MultiTenantTests
        {
            IApplication _app;

            [OneTimeSetUp]
            public void SetUp()
            {
                _app = AppFactory.Build<MulitTenant>(typeof(IChassisMarker).Assembly);
            }

            [Test]
            public void IsMultiTenant()
            {
                _app.Container.ShouldBeOfType<MultitenantContainer>();
                _app.LoadedTenants.Count().ShouldBe(1);
            }


            [Test]
            public void ShouldHaveApplicationInstance()
            {
                var app = _app.Resolve<IApplication>();
                app.ShouldNotBe(null);
            }

            [Test]
            public void ShouldHaveTenantUnique()
            {
                TenantUnique unique;
                _app.Container.TryResolve(out unique).ShouldBe(true);
            }
        }



        class SingleTenant : IApplicationMarker
        {
            public void ConfigureContainer(TypePool pool, ContainerBuilder builder)
            {

            }
        }

        class MulitTenant : IApplicationMarker
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
