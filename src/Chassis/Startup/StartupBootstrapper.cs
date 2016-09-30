using System.Collections.Generic;
using System.Web.Http;
using Chassis.Apps;

namespace Chassis.Startup
{
    public class StartupBootstrapper
    {
        readonly IEnumerable<IStartupStep> _startUps;

        public StartupBootstrapper(IEnumerable<IStartupStep> startUps)
        {
            _startUps = startUps;
        }

        public void LightItUp()
        {
            foreach (var startUp in _startUps)
            {
                startUp.Execute();
            }
        }
    }
}
