using Autofac;
using Chassis.Introspection;
using Chassis.Types;

namespace Chassis.Env
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterEnvironmentType<TService, TProdType, TTestType>(this ContainerBuilder builder)
            where TProdType : TService
            where TTestType : TService
        {
            builder.RegisterType<TProdType>().As<TProdType>();
            builder.RegisterType<TTestType>().As<TTestType>();

            var a = builder.Register<TService>(cxt =>
            {
                if (AppEnv.ENV == AppEnv.TEST) return cxt.Resolve<TTestType>();
                return cxt.Resolve<TProdType>();
            }).As<TService>();

            if (typeof (TService).Implements(typeof (IProbeSite)))
            {
                a.As<IProbeSite>();
            }
        }
    }
}
