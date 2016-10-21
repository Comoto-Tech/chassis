using System.Web.Http;
using Autofac.Integration.WebApi;
using Chassis.Apps;

namespace Chassis.Fairing.WebApi
{
    public class ChassisDependencyResolverStartupStep : IWebApiStartupStep
    {
        readonly IApplication _app;

        public ChassisDependencyResolverStartupStep(IApplication app)
        {
            _app = app;
        }

        public void Boot(HttpConfiguration cfg)
        {
            cfg.DependencyResolver = new AutofacWebApiDependencyResolver(_app.Container);
        }
    }
}
