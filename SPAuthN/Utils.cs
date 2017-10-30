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
            bool versionUpdate = false;
            string depPath = "auth/Dependencies.txt";
            string depInstalled = "Installed";
            if (File.Exists(depPath))
            {
                if (File.ReadAllText(depPath).ToString() != depInstalled)
                {
                    File.WriteAllText(depPath, depInstalled);
                    versionUpdate = true;
                }
            }
            return versionUpdate || !Directory.Exists("node_modules");
        }

        public static void NpmCheckAndInstall()
        {
            if (NeedToInstall())
            {
                Console.WriteLine("Running dependencies install, please wait...");
                File.Copy("auth/package.json", "package.json", true);
                if (File.Exists("package-lock.json")) 
                {
                    File.Delete("package-lock.json");
                }
                CmdExecTask("npm install").Wait();
                // File.Delete("auth/Dependencies.txt");
                File.Delete("package.json");
            }
        }

    }

}
