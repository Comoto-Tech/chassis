using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Chassis.Apps;

namespace Chassis.Fairing.Mvc
{
    public class SetResolverStartupStep : IMvcStartupStep
    {
        readonly IApplication _app;

        public SetResolverStartupStep(IApplication app)
        {
            _app = app;
        }

        public void Boot()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_app.Container));
        }
    }
}