using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Multitenant;
using Chassis.Features;
using Chassis.Tenants;
using Chassis.Types;
using Module = Autofac.Module;

namespace Chassis.Apps
{
    public class AppFactory
    {
        public static IApplication Build<TApplication>(params Assembly[] assemblies) where TApplication : IApplicationMarker, new()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var builder = new ContainerBuilder();

            var pool = new TypePool();

            pool.AddSource(typeof (TApplication).Assembly);
            pool.AddSource(typeof(IChassisMarker).Assembly);

            foreach (var assembly in assemblies)
            {
                pool.AddSource(assembly);
            }

            builder.RegisterInstance(pool)
                .As<TypePool>()
                .SingleInstance();

            //find all modules for this application
            var modules = (from module in pool.Query()
                where module.BaseType == typeof (Module)
                select Activator.CreateInstance(module))
                .Cast<Module>()
                .ToList();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            var features = (from feature in pool.Query()
                           where feature.BaseType == typeof(Feature)
                           where !feature.IsAbstract
                           select Activator.CreateInstance(feature))
                .Cast<Feature>()
                .ToList();

            foreach (var feature in features)
            {
                feature.RegisterComponents(builder, pool);
            }

            //application overrides
            var application = new TApplication();

            application.ConfigureContainer(pool, builder);

            var container = builder.Build();

            //tenant overrides
            var tenantOverrides = new List<TenantOverrides>();
            ITenantIdentificationStrategy strategy;
            if (container.TryResolve(out strategy))
            {
                var multi = new MultitenantContainer(strategy, container);

                tenantOverrides = (from @override in pool.Query()
                                       where @override.BaseType == typeof(TenantOverrides)
                                       where !@override.IsAbstract
                                       select Activator.CreateInstance(@override))
                   .Cast<TenantOverrides>()
                   .ToList();

                foreach (var @override in tenantOverrides)
                {
                    multi.ConfigureTenant(@override.TenantId, to =>
                    {
                        @override.RegisterComponents(to, pool);
                    });
                }

                container = multi;
            }

            stopwatch.Stop();

            var app = new ApplicationInstance(container,
                pool,
                features,
                modules,
                tenantOverrides,
                stopwatch.Elapsed);

            var cb = new ContainerBuilder();
            cb.RegisterInstance(app)
                .As<IApplication>()
                .SingleInstance();

            cb.Update(container);

            return app;
        }
    }
}
