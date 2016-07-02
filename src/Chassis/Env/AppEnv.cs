using System;
using System.Diagnostics;

namespace Chassis.Env
{
    [DebuggerDisplay("ENV:{_env}")]
    public class AppEnv
    {
        public const string EnvironmentFile = ".magenv";
        public const string EnvironmentVariable = "MAG_ENV";

        public static readonly AppEnv TEST = new AppEnv("TEST");
        public static readonly AppEnv PROD = new AppEnv("PRODUCTION");

        readonly string _env;

        AppEnv(string env)
        {
            _env = env;
        }

        public static AppEnv ENV
        {
            [DebuggerStepThrough]
            get
            {
                var envStr = Environment.GetEnvironmentVariable(EnvironmentVariable)
                    ?? environmentSpecifiedInAppConfig()
                    ?? environmentSpecifiedInFile();

                var env = TEST;
                if(envStr != null) env = new AppEnv(envStr.ToUpper());

                return env;
            }
        }

        static string environmentSpecifiedInAppConfig()
        {
            return System.Configuration.ConfigurationManager.AppSettings["AppEnv"];
        }

        static string environmentSpecifiedInFile()
        {
            return FS.GetFile(EnvironmentFile);
        }

        public static implicit operator AppEnv(string env)
        {
            return new AppEnv(env);
        }


        public static bool operator ==(AppEnv a, AppEnv b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a._env == b._env;
        }

        public static bool operator !=(AppEnv a, AppEnv b)
        {
            return !(a == b);
        }

        protected bool Equals(AppEnv other)
        {
            return string.Equals(_env, other._env);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AppEnv) obj);
        }

        public override int GetHashCode()
        {
            return (_env != null ? _env.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return _env;
        }

        public static void Set(string env)
        {
            if (ENV == TEST)
            {
                Environment.SetEnvironmentVariable(EnvironmentVariable, env);
            }
        }

        public static void Set(AppEnv env)
        {
            Set(env.ToString());
        }

        public static void Reset()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariable, TEST._env);
        }
    }
}
