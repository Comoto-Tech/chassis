using System;
using System.Collections.Generic;
using System.Threading;

namespace Chassis.Introspection
{
    public class ProbeResultBuilder :
        ScopeProbeContext,
        IProbeResultBuilder
    {
        readonly IHostInfo _host;
        readonly Guid _probeId;
        readonly Guid _resultId;
        readonly DateTime _startTimestamp;

        public ProbeResultBuilder(Guid probeId, CancellationToken cancellationToken)
            : base(cancellationToken)
        {
            _probeId = probeId;

            _resultId = Guid.NewGuid();
            _startTimestamp = DateTime.UtcNow;
            _host = HostMetadataCache.Host;
        }

        IProbeResult IProbeResultBuilder.Build()
        {
            TimeSpan duration = DateTime.UtcNow - _startTimestamp;

            return new Result(_probeId, _resultId, _startTimestamp, duration, _host, Build());
        }


        class Result :
            IProbeResult
        {
            public Result(Guid probeId, Guid resultId, DateTime startTimestamp, TimeSpan duration, IHostInfo host, IDictionary<string, object> results)
            {
                ProbeId = probeId;
                ResultId = resultId;
                StartTimestamp = startTimestamp;
                Duration = duration;
                Host = host;
                Results = results;
            }

            public Guid ResultId { get; set; }
            public Guid ProbeId { get; set;  }
            public DateTime StartTimestamp { get; set; }
            public TimeSpan Duration { get; set;  }
            public IHostInfo Host { get; set; }
            public IDictionary<string, object> Results { get; set; }
        }
    }
}
