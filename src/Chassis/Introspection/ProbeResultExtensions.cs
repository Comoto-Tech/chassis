using System.Collections.Generic;
using System.Linq;

namespace Chassis.Introspection
{
    public static class ProbeResultExtensions
    {
        public static bool AllOk(this IProbeResult probeResult)
        {
            var flattenedResults = probeResult.Results.SelectMany(Flatten);
            var keys = flattenedResults.Where(r => r.Key == "status").ToList();
            var ok = keys.All(r => r.Value.ToString() == "ok");
            return ok;
        }

        static IEnumerable<KeyValuePair<string, object>> Flatten(KeyValuePair<string, object> arg)
        {
            if (arg.Value is IEnumerable<KeyValuePair<string, object>>)
            {
                return (IEnumerable<KeyValuePair<string, object>>)arg.Value;
            }

            return new List<KeyValuePair<string, object>> {arg};
        }
    }
}
