using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace SPAuthN.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();

            sw.Start();

            string SiteUrl = Environment.GetEnvironmentVariable("SPAUTH_SITEURL");
            string Username = Environment.GetEnvironmentVariable("SPAUTH_USERNAME");
            string Password = Environment.GetEnvironmentVariable("SPAUTH_PASSWORD");

            Options options = null;

            if (SiteUrl != null && Username != null && Password != null)
            {
                options = SPAuth.GetAuth($@"--authOptions.siteUrl='{SiteUrl}' --authOptions.username='{Username}' --authOptions.password='{Password}' --saveConfigOnDisk=false");
            }
            else
            {
                options = SPAuth.GetAuth("--configPath='./config/private.json' --forcePrompts=true");
            }

            sw.Stop(); Console.WriteLine("Auth time: " + sw.Elapsed);

            /* REST Example */

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(options.SiteUrl + "/_api/web?$select=Title");
            Request.ApplyAuth(request, options);

            request.Method = "GET";
            request.Accept = "application/json; odata=verbose";

            sw.Restart();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string strResponse = reader.ReadToEnd();
                            dynamic results = JsonConvert.DeserializeObject(strResponse);

                            Console.WriteLine("REST | Web title is: {0}", results.d.Title);
                        }
                    }
                }
            }
            sw.Stop(); Console.WriteLine("REST request time: " + sw.Elapsed);

            /* CSOM Example */

            sw.Restart();
            using (ClientContext clientContext = new ClientContext(options.SiteUrl))
            {
                Request.ApplyAuth<WebRequestEventArgs>(clientContext, options);

                var web = clientContext.Web;
                clientContext.Load(web);
                clientContext.ExecuteQuery();

                // System.Threading.Thread.Sleep(1 * 1000);
                Console.WriteLine("CSOM | Web title is: {0}", web.Title);

                // Endless loop
                /*
                while (true)
                {
                    var web1 = clientContext.Web;
                    clientContext.Load(web1);
                    clientContext.ExecuteQuery();
                    Console.WriteLine("CSOM | Web title is: {0}", web1.Title);
                    System.Threading.Thread.Sleep(1 * 1000);
                }
                */
            }
            sw.Stop(); Console.WriteLine("CSOM request time: " + sw.Elapsed);


            var bp = ""; bp += "";

        }
    }
}
