using System;
using System.IO;

namespace Chassis.Env
{
    internal class FS
    {
        public static string GetFile(string path)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var x = Path.Combine(root, path);
            if (System.IO.File.Exists(x))
            {
                return System.IO.File.ReadAllText(x);
            }

            return null;
        }

        public static void WriteFile(string path, string contents)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var x = Path.Combine(root, path);
            System.IO.File.AppendAllText(x, contents);
        }

        public static void DeleteFile(string path)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var x = Path.Combine(root, path);
            if (System.IO.File.Exists(x))
            {
                System.IO.File.Delete(x);
            }
        }
    }
}