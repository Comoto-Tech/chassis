using System;

namespace Chassis.Types
{
    public interface TypeScan
    {
        bool Matches(Type type);
    }
}