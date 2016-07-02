using System;
using System.Diagnostics;
using System.Reflection;

namespace Chassis.Introspection
{
    [Serializable]
    public class ChassisHostInfo :
        IHostInfo
    {
        public ChassisHostInfo()
        {
            MachineName = Environment.MachineName;

            ChassisVersion = FileVersionInfo.GetVersionInfo(typeof(IChassisMarker).Assembly.Location).FileVersion;
            FrameworkVersion = Environment.Version.ToString();
            OperatingSystemVersion = Environment.OSVersion.ToString();
            Process currentProcess = Process.GetCurrentProcess();
            ProcessId = currentProcess.Id;
            ProcessName = currentProcess.ProcessName;

            Assembly entryAssembly = System.Reflection.Assembly.GetEntryAssembly() ?? System.Reflection.Assembly.GetCallingAssembly();
            AssemblyName assemblyName = entryAssembly.GetName();
            Assembly = assemblyName.Name;
            AssemblyVersion = FileVersionInfo.GetVersionInfo(entryAssembly.Location).FileVersion;

            IsDebug = Debugger.IsAttached;
            Processor = Environment.Is64BitProcess ? "x64" : "x32";
        }


        public string MachineName { get; set; }
        public string ProcessName { get; set; }
        public int ProcessId { get; set; }
        public string Assembly { get; set; }
        public string AssemblyVersion { get; set; }
        public string FrameworkVersion { get; set; }
        public string ChassisVersion { get; set; }
        public string OperatingSystemVersion { get; set; }


        public bool IsDebug { get; set; }
        public string Processor { get; set; }
    }
}
