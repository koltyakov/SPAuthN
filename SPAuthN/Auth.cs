using EdgeJs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SPAuthN
{

    public class SPAuth
    {
        public static async Task<object> AuthTask(string args = "")
        {
            var func = Edge.Func("return " + File.ReadAllText("auth/SPAuthN.js"));
            return await func(args);
        }

        public static Headers GetAuth(string args = "")
        {
            Utils.NpmInstall();
            string authHeadersStr = (string)AuthTask(args).Result;

            Headers authHeaders = new Headers();

            string[] headersArr = authHeadersStr.Split(new[] { ";;" }, StringSplitOptions.None);
            for (int i = 0; i < headersArr.Length; i++)
            {
                string[] header = headersArr[i].Split(new[] { "::" }, StringSplitOptions.None);
                authHeaders.GetType().GetProperty(header[0]).SetValue(authHeaders, header[1]);
            }

            return authHeaders;
        }
    }

}
