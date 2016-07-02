using System;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Chassis.Introspection
{
    public static class ProbeSiteExtensions
    {
        public static IProbeResult GetProbeResult(this IProbeSite probeSite,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var builder = new ProbeResultBuilder(Guid.NewGuid(), cancellationToken);

            probeSite.Probe(builder);

            return ((IProbeResultBuilder) builder).Build();
        }

        public static string ToJsonString(this IProbeResult result)
        {
            var encoding = new UTF8Encoding(false, true);

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, encoding, 1024, true))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.Formatting = Formatting.Indented;

                new JsonSerializer().Serialize(jsonWriter, result, typeof (IProbeResult));

                jsonWriter.Flush();
                writer.Flush();

                return encoding.GetString(stream.ToArray());
            }
        }


        public static void StatusOk(this IProbeContext context)
        {
            context.Status("ok");
        }

        public static void Status(this IProbeContext context, string status)
        {
            context.Add("status", status);
        }
    }
}
