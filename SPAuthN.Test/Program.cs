using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace SPAuthN.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Options options = SPAuth.GetAuth("--configPath='./config/private.json'");

            /* REST Example */

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(options.SiteUrl + "/_api/web?$select=Title");
            Request.ApplyAuth(request, options);

            request.Method = "GET";
            request.Accept = "application/json; odata=verbose";

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

            /* CSOM Example */

            using (ClientContext clientContext = new ClientContext(options.SiteUrl))
            {
                Request.ApplyAuth<WebRequestEventArgs>(clientContext, options);

                var web = clientContext.Web;
                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Console.WriteLine("CSOM | Web title is: {0}", web.Title);
            }


            var bp = ""; bp += "";

        }
    }
}
