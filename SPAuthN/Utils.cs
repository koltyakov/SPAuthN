using EdgeJs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SPAuthN
{

    public class Utils
    {

        public static async Task CmdExecTask(string cmd)
        {
            var func = Edge.Func("return " + File.ReadAllText("auth/scripts/exec.js"));
            await func(cmd);
        }

        public static bool NeedToInstall()
        {
            return File.Exists("auth/Dependencies.txt") || !Directory.Exists("node_modules");
        }

        public static void NpmInstall()
        {
            if (NeedToInstall())
            {
                Console.WriteLine("Running dependencies install, please wait...");
                if (File.Exists("package-lock.json")) 
                {
                    File.Delete("package-lock.json");
                }
                File.Copy("auth/package.json", "package.json", true);
                CmdExecTask("npm install").Wait();
                File.Delete("auth/Dependencies.txt");
                File.Delete("package.json");
            }
        }

    }

}
