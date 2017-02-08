using System.Threading.Tasks;
using System.Web.Http;

namespace Chassis.Startup
{
    public interface IWebApiStartupStep
    {
        Task Configure(HttpConfiguration cfg);
    }
}
