using System.Web.Http;

namespace Chassis.Fairing.WebApi
{
    public interface IWebApiStartupStep
    {
        void Boot(HttpConfiguration cfg);
    }
}