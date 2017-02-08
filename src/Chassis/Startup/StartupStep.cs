using System.Threading.Tasks;

namespace Chassis.Startup
{
    public interface IStartupStep
    {
        Task Execute();
    }
}
