using EdgeJs;
using System.IO;
using System.Threading.Tasks;

namespace SPAuthN {
    class Utils {

        public static async Task CmdExecTask(string cmd) {
            var func = Edge.Func("return " + File.ReadAllText("auth/scripts/exec.js"));
            await func(cmd);
        }

        public static bool InstallDependencies() {
            return File.Exists("Dependencies.txt");
        }

        public static void NpmInstall() {
            if (InstallDependencies()) {
                CmdExecTask("npm install").Wait();
                File.Delete("Dependencies.txt");
            }
        }

    }
}
