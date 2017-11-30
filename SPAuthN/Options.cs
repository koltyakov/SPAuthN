using System;
using System.Collections.Generic;
using System.Net;

namespace SPAuthN
{

    public class Options
    {
        private DateTime _timestamp;

        public Options(dynamic context)
        {
            _timestamp = DateTime.UtcNow;


            SiteUrl = context.siteUrl;
            Strategy = context.strategy;
            AuthOptions = context.authOptions;

            Settings = context.settings;
            Custom = context.custom;

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
