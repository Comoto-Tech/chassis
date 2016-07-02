namespace Chassis.Apps
{
    public interface IDispatcher
    {
        object Dispatch(IRequest request);
    }
}