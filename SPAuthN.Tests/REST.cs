using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SPAuthN.Tests
{
    [TestClass]
    public class REST
    {
        //[TestMethod]
        //public void CanAuthAndRequestRestApi()
        //{
        //    Options options = Helpers.GetAuthOptions();

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(options.SiteUrl + "/_api/web?$select=Title");
        //    Request.ApplyAuth(request, options);

        //    request.Method = "GET";
        //    request.Accept = "application/json; odata=verbose";

        //    string WebTitle = null;

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
        //        {
        //            using (Stream dataStream = response.GetResponseStream())
        //            {
        //                using (StreamReader reader = new StreamReader(dataStream))
        //                {
        //                    string strResponse = reader.ReadToEnd();
        //                    dynamic results = JsonConvert.DeserializeObject(strResponse);

        //                    WebTitle = results.d.Title;
        //                    Console.WriteLine("REST | Web title is: {0}", results.d.Title);
        //                }
        //            }
        //        }
        //    }

        //    Assert.IsNotNull(WebTitle);
        //}
    }
}
