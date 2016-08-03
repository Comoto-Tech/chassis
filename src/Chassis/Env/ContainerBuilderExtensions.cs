using System;
using Autofac;
using Chassis.Introspection;
using Chassis.Types;

namespace Chassis.Env
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterEnvironmentType<TService, TProdType, TTestType>(this ContainerBuilder builder)
            where TService : class
            where TProdType : class, TService
            where TTestType : class, TService
        {

            builder.RegisterEnvironmentType<TService>(e =>
            {
                e.On<TProdType>(AppEnv.PRODUCTION);
                e.On<TTestType>(AppEnv.TEST);
            });
        }

        public static void RegisterEnvironmentType<TService>(this ContainerBuilder builder, Action<EnvSwitcher<TService>> action)
            where TService : class
        {
            var es = new EnvSwitcher<TService>();
            action(es);

            es.RegisterTypes(builder);

            var a = builder.Register(cxt => es.Resolve(cxt));

            if (typeof(TService).Implements(typeof(IProbeSite)))
            {
                a.As<IProbeSite>();
            }
        }
    }
}
