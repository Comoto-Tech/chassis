using System;

namespace Chassis.Types
{
    public class ClosedInfo
    {
        public Type OpenType { get; set; }
        public Type ClosedType { get; set; }
        public Type[] GenericArgs { get; set; }
    }
}