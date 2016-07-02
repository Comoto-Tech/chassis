using System;

namespace Chassis.Apps
{
    public interface IRequest
    {
        string Command { get; }
        Guid RequestId { get; }
    }
}
