using System.Web.Mvc;
using System.Web.Routing;

namespace Chassis.Fairing.Mvc
{
    public class DefaultRouting : IMvcStartupStep
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