using System.Web.Http;

namespace Chassis.Startup
{
    public interface IWebStartupStep
    {
        void Configure(HttpConfiguration cfg);
    }
}
