using System;
using System.Collections.Generic;
using Autofac;

namespace Chassis.Env
{
    public class EnvSwitcher<TService>
        where TService : class
    {
        readonly List<Type> _types = new List<Type>();
        readonly Dictionary<AppEnv, Type> _maps = new Dictionary<AppEnv, Type>();

        public void On<TType>(AppEnv env)
            where TType : class
        {
            _types.Add(typeof(TType));
            _maps.Add(env, typeof(TType));
        }

        internal void RegisterTypes(ContainerBuilder builder)
        {
            foreach (var type in _types)
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
