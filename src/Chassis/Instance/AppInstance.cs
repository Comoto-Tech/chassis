using System;
using System.Diagnostics;

namespace Chassis.Instance
{
    [DebuggerDisplay("Instance:{_instance}")]
    public class AppInstance
    {
        public const string EnvironmentVariable = "CHASSIS_INSTANCE";

        public static AppInstance DEFAULT = new AppInstance("00");

        readonly string _instance;

        public AppInstance(string instance)
        {
            _instance = instance;
        }

        public static AppInstance INSTANCE
        {
            [DebuggerStepThrough]
            get
            {
                var envStr = Environment.GetEnvironmentVariable(EnvironmentVariable)
                    ?? environmentSpecifiedInAppConfig();

                var env = DEFAULT;
                if(envStr != null) env = new AppInstance(envStr.ToUpper());

                return env;
            }
        }

        static string environmentSpecifiedInAppConfig()
        {
            return System.Configuration.ConfigurationManager.AppSettings["AppInstance"];
        }

        public static implicit operator AppInstance(string env)
        {
            return new AppInstance(env);
        }


        public static bool operator ==(AppInstance a, AppInstance b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a._instance == b._instance;
        }

        public static bool operator !=(AppInstance a, AppInstance b)
        {
            return !(a == b);
        }

        protected bool Equals(AppInstance other)
        {
            return string.Equals(_instance, other._instance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AppInstance) obj);
        }

        public override int GetHashCode()
        {
            return (_instance != null ? _instance.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return _instance;
        }
    }
}
