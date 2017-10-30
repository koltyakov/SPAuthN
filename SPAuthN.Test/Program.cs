using Microsoft.SharePoint.Client;
using System;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace SPAuthN.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Options options = SPAuth.GetAuth("--configPath='./config/private.json'");

            Console.WriteLine("SharePoint web URL: {0}", options.SiteUrl);
            foreach (var key in options.Headers.AllKeys)
            {
                Console.WriteLine("\nHeader: {0} \nValue: {1}", key, options.Headers[key]);
            }
            Console.WriteLine("");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(options.SiteUrl + "/_api/web?$select=Title");
            Request.ApplyAuth(request, options);

            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
            {
                Stream dataStream = response.GetResponseStream();
                XDocument xDoc = XDocument.Load(dataStream);

                XNamespace ns = "http://www.w3.org/2005/Atom";
                XNamespace d = "http://schemas.microsoft.com/ado/2007/08/dataservices";
                XNamespace m = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

                string title = xDoc
                    .Element(ns + "entry").Element(ns + "content")
                    .Element(m + "properties").Element(d + "Title").Value;

                Console.WriteLine("REST | Web title is: {0}", title);

                dataStream.Close();
            }

            response.Close();

            using (ClientContext clientContext = new ClientContext(options.SiteUrl))
            {
                Request.ApplyAuth<WebRequestEventArgs>(clientContext, options);

                var web = clientContext.Web;
                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Console.WriteLine("CSOM | Web title is: {0}", web.Title);
            }


            var bp = "";

        }
    }
}
