using EdgeJs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace SPAuthN
{

    public class SPAuth
    {
        public static async Task<object> AuthTask(string args = "")
        {
            var func = Edge.Func("return " + File.ReadAllText("auth/SPAuthN.js"));
            return await func(args);
        }

        public static Options GetAuth(string args = "")
        {
            return GetAuth(args, true);
        }

        public static Options GetAuth(string args = "", Boolean checkDependencies = true)
        {
            if (checkDependencies)
            {
                Utils.NpmCheckAndInstall();
            }
            dynamic context = AuthTask(args).Result;
            return new Options(context);
        }

    }

}
