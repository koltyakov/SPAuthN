﻿using System;
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
            else
            {
                foreach (var key in options.Headers.AllKeys)
                {
                    if (!WebHeaderCollection.IsRestricted(key))
                    {
                        request.Headers.Add(key, options.Headers[key]);
                    }
                }
                request.Headers.Add("Origin", options.SiteUrl);
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
            else
            {
                foreach (var key in options.Headers.AllKeys)
                {
                    if (!WebHeaderCollection.IsRestricted(key))
                    {
                        webRequestExecutor.RequestHeaders.Add(key, options.Headers[key]);
                    }
                }
                webRequestExecutor.RequestHeaders.Add("Origin", options.SiteUrl);
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
            else
            {
                clientContext.ExecutingWebRequest += new EventHandler<T>((sender, arguments) =>
                {
                    foreach (var key in options.Headers.AllKeys)
                    {
                        if (!WebHeaderCollection.IsRestricted(key))
                        {
                            ((dynamic)arguments).WebRequestExecutor.RequestHeaders.Add(key, options.Headers[key]);
                        }
                    }
                    ((dynamic)arguments).WebRequestExecutor.RequestHeaders.Add("Origin", options.SiteUrl);
                });
            }
        }


    }
}
