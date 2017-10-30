using System.Net;

namespace SPAuthN
{

    public class Options
    {
        public string SiteUrl { get; set; }

        public string Strategy { get; set; }

        public dynamic AuthOptions { get; set; }

        public WebHeaderCollection Headers { get; set; }

    }

}
