using System;
using System.Net.Http;
using System.Threading.Tasks;
using Chassis.Startup;

namespace Chassis.Integration.Consul
{
    public class ConsulStartupStep : IStartupStep
    {
        public Task Execute()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8500");

                // register thingy
                var x = client.PutAsJsonAsync("/v1/agent/service/register", new ConsulRegister
                {
                    ID = "abc01",
                    Name = "abc",
                    Tags = new []{"ABC"},
                    Address = "MACHINE-NAME",
                    Port = 8080,
                    EnableTagOverride = false,
                    Check = new ConsulHealthCheck
                    {
                        HTTP = "http://localhost:8080",
                        Interval = "10m",
                    }
                }).Result;
                // set up a health check endpoint
            }

            return Task.FromResult(true);
        }
    }

    public class ConsulRegister
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public bool EnableTagOverride { get; set; }
        public ConsulHealthCheck Check { get; set; }
    }

    public class ConsulHealthCheck
    {
        public string DeregisterCriticalServiceAfter { get; set; }
        public string HTTP { get; set; }
        public string Interval { get; set; }

    }
}
