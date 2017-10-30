using System;
using System.Net;

namespace SPAuthN
{
    public class Request
    {
        public static void ApplyAuth(HttpWebRequest request, Options options)
        {

            if (options.Strategy == "OnpremiseUserCredentials")
            {
                var auth = options.AuthOptions;
                CredentialCache credCache = new CredentialCache();
                credCache.Add(new Uri(options.SiteUrl), "NTLM", new NetworkCredential(auth.username, auth.password, auth.domain));
                request.Credentials = credCache;
            }

            foreach (var key in options.Headers.AllKeys)
            {
                if (key.ToLower() == "connection")
                {
                    request.KeepAlive = options.Headers[key].ToLower() == "close" ? false : true;
                    continue;
                }
                if (key.ToLower() == "authorization")
                {
                    // request.UnsafeAuthenticatedConnectionSharing = true;
                    // request.PreAuthenticate = true;               
                    // request.UseDefaultCredentials = true;
                    continue;
                }
                if (!WebHeaderCollection.IsRestricted(key))
                {
                    request.Headers.Add(key, options.Headers[key]);
                }
            }
        }

        public static void ApplyAuth(dynamic clientContext, dynamic webRequestExecutor, Options options)
        {
            if (options.Strategy == "OnpremiseUserCredentials")
            {
                var auth = options.AuthOptions;
                CredentialCache credCache = new CredentialCache();
                credCache.Add(new Uri(options.SiteUrl), "NTLM", new NetworkCredential(auth.username, auth.password, auth.domain));
                clientContext.Credentials = credCache;
            }

            foreach (var key in options.Headers.AllKeys)
            {
                if (key.ToLower() == "connection" || key.ToLower() == "authorization")
                {
                    continue;
                }
                if (!WebHeaderCollection.IsRestricted(key))
                {
                    webRequestExecutor.RequestHeaders.Add(key, options.Headers[key]);
                }
            }
        }

        public static void ApplyAuth<T>(dynamic clientContext, Options options)
        {
            if (options.Strategy == "OnpremiseUserCredentials")
            {
                var auth = options.AuthOptions;
                CredentialCache credCache = new CredentialCache();
                credCache.Add(new Uri(options.SiteUrl), "NTLM", new NetworkCredential(auth.username, auth.password, auth.domain));
                clientContext.Credentials = credCache;
            }

            clientContext.ExecutingWebRequest += new EventHandler<T>((sender, arguments) =>
            {
                foreach (var key in options.Headers.AllKeys)
                {
                    if (key.ToLower() == "connection" || key.ToLower() == "authorization")
                    {
                        continue;
                    }
                    if (!WebHeaderCollection.IsRestricted(key))
                    {
                        ((dynamic)arguments).WebRequestExecutor.RequestHeaders.Add(key, options.Headers[key]);
                    }
                }
            });

        }


    }
}
