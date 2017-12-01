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

    public class Options
    {
        private DateTime _timestamp;

        public Options(dynamic context)
        {
            _timestamp = DateTime.UtcNow;


            SiteUrl = context.siteUrl;
            Strategy = context.strategy;
            AuthOptions = context.authOptions;

            try
            {
                Settings = context.settings;
                Custom = context.custom;
            }
            catch
            {
            }

            Headers = new WebHeaderCollection();

            foreach (var property in (IDictionary<String, Object>)context.headers)
            {
                Headers.Add((string)property.Key, (string)property.Value);
            }
        }

        public string SiteUrl { get; set; }

        public string Strategy { get; set; }

        public dynamic AuthOptions { get; set; }

        public dynamic Settings { get; set; }

        public dynamic Custom { get; set; }

        public WebHeaderCollection Headers { get; set; }

        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
        }
    }

}
