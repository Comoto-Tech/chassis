using System.Web.Http;

namespace Chassis.Fairing.WebApi
{
    public class AttributeRoutesStartupStep : IWebApiStartupStep
    {
        public void Boot(HttpConfiguration cfg)
        {
            cfg.MapHttpAttributeRoutes();

            cfg.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}