using System.Collections.Generic;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Chassis.Apps;

namespace Chassis.Fairing.Mvc
{
    public static class ChassisMvcBootstrapper
    {
        public static void Bootstrap(IApplication app)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(app.Container));

            foreach (var step in app.Resolve<IEnumerable<IMvcStartupStep>>())
            {
                step.Boot();
            }
        }
    }
}
