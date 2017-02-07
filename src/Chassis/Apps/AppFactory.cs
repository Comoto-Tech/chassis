using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Multitenant;
using Chassis.Features;
using Chassis.Meta;
using Chassis.Tenants;
using Chassis.Types;
using Module = Autofac.Module;

namespace Chassis.Apps
{
    public class AppFactory
    {
        public static async Task<IApplicationInstance> Build<TApplication>(params Assembly[] assemblies) where TApplication : IApplicationDefinition, new()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var builder = new ContainerBuilder();

            var pool = new TypePool();

            pool.AddSource(typeof(TApplication).Assembly);
            pool.AddSource(typeof(IChassisMarker).Assembly);

            foreach (var assembly in assemblies)
            {
                pool.AddSource(assembly);
            }

            builder.RegisterInstance(pool)
                .As<TypePool>()
                .SingleInstance();

            var modules = FindAllModules(pool);
            var features = FindAllFeatures(pool);
            await Task.WhenAll(modules, features).ConfigureAwait(false);

            foreach (var module in modules.Result)
            {
                builder.RegisterModule(module);
            }

            foreach (var feature in features.Result)
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

                tenantOverrides = await FindAllTenantOverrides(pool).ConfigureAwait(false);

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
            var md = new ApplicationMetaData(application, modules.Result, features.Result, tenantOverrides, stopwatch.Elapsed);
            var app = new ApplicationInstance(container, pool);

            /*
             * In the near future Autofac will start to disallow the mutation of
             * a container after it has been created.
             * https://github.com/autofac/Autofac/issues/811
             */
            //TODO: Remove mutation of container after creation.
            var cb = new ContainerBuilder();
            cb.RegisterInstance(app)
                .As<IApplicationInstance>()
                .SingleInstance();

            cb.RegisterInstance(md);

            cb.Update(container);

            return app;
        }

        static Task<List<Module>> FindAllModules(TypePool pool)
        {
            var modules = (from module in pool.Query()
                           where module.BaseType == typeof(Module)
                           select Activator.CreateInstance(module))
                .Cast<Module>()
                .ToList();

            return Task.FromResult(modules.ToList());
        }

        static Task<List<Feature>> FindAllFeatures(TypePool pool)
        {
            var features =  (from feature in pool.Query()
                    where feature.BaseType == typeof(Feature)
                    where !feature.IsAbstract
                    select Activator.CreateInstance(feature))
                .Cast<Feature>()
                .ToList();

            return Task.FromResult(features);
        }

        static Task<List<TenantOverrides>> FindAllTenantOverrides(TypePool pool)
        {
            var tenantOverrides = (from @override in pool.Query()
                    where @override.BaseType == typeof(TenantOverrides)
                    where !@override.IsAbstract
                    select Activator.CreateInstance(@override))
                .Cast<TenantOverrides>()
                .ToList();

            return Task.FromResult(tenantOverrides);
        }
    }
}
