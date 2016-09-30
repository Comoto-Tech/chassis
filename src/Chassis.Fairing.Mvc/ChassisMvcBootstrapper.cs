using System.Collections.Generic;
using Chassis.Apps;

namespace Chassis.Fairing.Mvc
{
    public static class ChassisMvcBootstrapper
    {
        public static void Bootstrap(IApplication app)
        {
            foreach (var step in app.Resolve<IEnumerable<IMvcStartupStep>>())
            {
                step.Boot();
            }
        }
    }
}
