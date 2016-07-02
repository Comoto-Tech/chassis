namespace Chassis.Introspection
{
    public static class HostMetadataCache
    {
        public static IHostInfo Host { get { return Cached.HostInfo; } }


        static class Cached
        {
            internal static readonly IHostInfo HostInfo = new ChassisHostInfo();
        }
    }
}
