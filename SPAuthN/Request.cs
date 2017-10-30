using System.Net;

namespace SPAuthN
{
    public class Request
    {
        public static void ApplyAuth(HttpWebRequest request, Options options)
        {
            foreach (var key in options.Headers.AllKeys)
            {
                if (key.ToLower() == "connection")
                {
                    request.KeepAlive = options.Headers[key].ToLower() == "close" ? false : true;
                    continue;
                }
                if (key.ToLower() == "authorization")
                {
                    request.UseDefaultCredentials = true;
                }
                if (!WebHeaderCollection.IsRestricted(key))
                {
                    request.Headers.Add(key, options.Headers[key]);
                }
            }
        }

        public static void ApplyAuth(dynamic webRequestExecutor, Options options)
        {
            foreach (var key in options.Headers.AllKeys)
            {
                if (key.ToLower() == "connection")
                {
                    continue;
                }
                if (!WebHeaderCollection.IsRestricted(key))
                {
                    webRequestExecutor.RequestHeaders.Add(key, options.Headers[key]);
                }
            }
        }
    }
}
