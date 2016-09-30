using System.Web.Mvc;
using System.Web.Routing;

namespace Chassis.Fairing.Mvc
{
    public class DefaultRoutingStartupStep : IMvcStartupStep
    {
        public void Boot()
        {
            AreaRegistration.RegisterAllAreas();
            var routes = RouteTable.Routes;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
        }
    }
}
