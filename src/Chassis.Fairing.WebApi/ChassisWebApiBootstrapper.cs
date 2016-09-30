using System.Collections.Generic;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Chassis.Apps;

namespace Chassis.Fairing.WebApi
{
    public static class ChassisWebApiBootstrapper
    {
        public static void Bootstrap(IApplication app, HttpConfiguration cfg)
        {
            cfg.DependencyResolver = new AutofacWebApiDependencyResolver(app.Container);
            foreach (var v in app.Resolve<IEnumerable<IWebApiStartupStep>>())
            {
                v.Boot(cfg);
            }
        }
    }
}
