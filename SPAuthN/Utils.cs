using EdgeJs;
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
                File.Copy("auth/package.json", "package.json");
                CmdExecTask("npm install").Wait();
                File.Delete("auth/Dependencies.txt");
                File.Delete("package.json");
            }
        }

    }

}
