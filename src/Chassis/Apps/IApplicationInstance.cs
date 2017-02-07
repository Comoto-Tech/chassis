using System;
using Autofac;
using Chassis.Introspection;

namespace Chassis.Apps
{
    public interface IApplicationInstance : IDisposable, IProbeSite
    {
        void Start();

        // TODO: Remove this items below

        IContainer Container { get; }
        TComponent Resolve<TComponent>();
        TComponent Resolve<TComponent>(string named);
        void Scope(Action<ILifetimeScope> scope);
        TResponse Dispatch<TRequest, TResponse>(TRequest request) where TRequest : class, IRequest;
    }
}
