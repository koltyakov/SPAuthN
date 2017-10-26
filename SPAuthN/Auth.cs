using EdgeJs;
using System.IO;
using System.Threading.Tasks;

namespace SPAuthN
{
    public class SP
    {
        public static async Task<object> AuthTask(string args = "") {
            var func = Edge.Func("return " + File.ReadAllText("auth/SPAuthN.js"));
            return await func(args);
        }

        public static string GetAuth(string args = "") {
            Utils.NpmInstall();
            string authHeaders = (string)AuthTask(args).Result;
            return authHeaders;
        }
    }
}
