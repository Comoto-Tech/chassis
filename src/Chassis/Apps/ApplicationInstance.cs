using System;
using System.Collections.Generic;
using Autofac;
using Chassis.Env;
using Chassis.Introspection;
using Chassis.Startup;
using Chassis.Types;

namespace Chassis.Apps
{
    public class ApplicationInstance : IApplicationInstance, Rebootable
    {
        readonly IContainer _container;
        readonly TypePool _pool;

        public ApplicationInstance(IContainer container,
            TypePool pool)
        {
            _container = container;

            _pool = pool;
        }

        public void Start()
        {
            //_container.Resolve<DbMigrationRunner>().MigrateToLatest();

            _container.Resolve<StartupBootstrapper>().LightItUp();
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string named)
        {
            return _container.ResolveNamed<T>(named);
        }

        public void Scope(Action<ILifetimeScope> action)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                action(scope);
            }
        }

        public TReponse Dispatch<TRequest, TReponse>(TRequest request) where TRequest : class, IRequest
        {
            using (var scope = _container.BeginLifetimeScope(cfg =>
            {
                cfg.RegisterInstance(request);
            }))
            {
                var dispatch = scope.Resolve<IDispatcher>();
                var response = dispatch.Dispatch(request);

                if (response is TReponse)
                {
                    return (TReponse) response;
                }
            }
            //find handler for this request
            //request handle
            //find responder - respond with response

            return default(TReponse);
        }


        //Implemented explicitly as this is a destructive action
        void Rebootable.Reboot()
        {
            if (AppEnv.ENV.Equals(AppEnv.PRODUCTION))
            {
                //yeah, this isn't going to happen in production
                return;
            }

            //_container.Resolve<DatabaseUtilities>().DropDatabase();
            //_container.Resolve<DbMigrationRunner>().MigrateToLatest();
        }

        //the types used when scanning in the various types
        public TypePool Pool => _pool;

        public IContainer Container => _container;


        public void Probe(IProbeContext context)
        {
            var sites = Container.Resolve<IEnumerable<IProbeSite>>();
            foreach (var probeSite in sites)
            {
                probeSite.Probe(context);
            }
        }
    }
}
