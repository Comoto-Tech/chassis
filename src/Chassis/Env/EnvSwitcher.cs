using System;
using System.Collections.Generic;
using Autofac;

namespace Chassis.Env
{
    public class EnvSwitcher<TService>
        where TService : class
    {
        readonly Dictionary<AppEnv, Type> _maps = new Dictionary<AppEnv, Type>();

        public void On<TType>(AppEnv env)
            where TType : class
        {
            if (_maps.ContainsKey(env))
            {
                throw new Exception($"The '{env}' environment already has '{_maps[env].Name}' registered for the service '{typeof(TService).Name}'.");
            }

            _maps.Add(env, typeof(TType));
        }

        internal void RegisterTypes(ContainerBuilder builder)
        {
            foreach (var type in _maps.Values)
            {
                builder.RegisterType(type).AsSelf();
            }
        }

        internal TService Resolve(IComponentContext cxt)
        {
            if (_maps.ContainsKey(AppEnv.ENV))
            {
                var t = _maps[AppEnv.ENV];
                return cxt.Resolve(t) as TService;
            }

            throw new Exception($"The container does not have an environment registration for '{AppEnv.ENV}' for service type {typeof(TService).Name}");
        }
    }
}
