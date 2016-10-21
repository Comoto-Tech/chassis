using System.Collections.Generic;
using System.Web.Http;
using Chassis.Apps;

namespace Chassis.Fairing.WebApi
{
    public static class ChassisWebApiBootstrapper
    {
        public static void Bootstrap(IApplication app, HttpConfiguration cfg)
        {
            foreach (var v in app.Resolve<IEnumerable<IWebApiStartupStep>>())
            {
                v.Boot(cfg);
            }
        }
    }
}
