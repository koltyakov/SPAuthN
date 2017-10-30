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
            Utils.NpmCheckAndInstall();
            dynamic context = AuthTask(args).Result;

            Options options = new Options();

            options.SiteUrl = context.siteUrl;
            options.Strategy = context.strategy;
            options.AuthOptions = context.authOptions;
            options.Headers = new WebHeaderCollection();

            foreach (var property in (IDictionary<String, Object>)context.headers)
            {
                options.Headers.Add((string)property.Key, (string)property.Value);
            }

            return options;
        }

    }

}
