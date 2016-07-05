using System;
using System.Collections.Generic;
using Autofac;
using Chassis.Features;
using Chassis.Introspection;
using Chassis.Tenants;

namespace Chassis.Apps
{
    public interface IApplication : IDisposable, IProbeSite
    {
        IContainer Container { get; }
        void Start();
        TComponent Resolve<TComponent>();
        TComponent Resolve<TComponent>(string named);
        void Scope(Action<ILifetimeScope> scope);
        TResponse Dispatch<TRequest, TResponse>(TRequest request) where TRequest : class, IRequest;

        IEnumerable<Module> LoadedModules { get; }
        IEnumerable<Feature> LoadedFeatures { get; }
        IEnumerable<TenantOverrides> LoadedTenants { get; }
    }
}
