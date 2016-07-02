﻿namespace Chassis.Introspection
{
    /// <summary>
    /// To support the introspection of code, this interface is used to gain
    /// information about the bus.
    /// </summary>
    public interface IProbeSite
    {
        void Probe(IProbeContext context);
    }
}