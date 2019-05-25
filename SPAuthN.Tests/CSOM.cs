using Microsoft.SharePoint.Client;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SPAuthN.Tests
{
    [TestClass]
    public class CSOM
    {
        //[TestMethod]
        //public void CanAuthAndRequestCsomApi()
        //{
        //    Options options = Helpers.GetAuthOptions();

        //    string WebTitle = null;

        //    using (ClientContext clientContext = new ClientContext(options.SiteUrl))
        //    {
        //        Request.ApplyAuth<WebRequestEventArgs>(clientContext, options);

        //        var web = clientContext.Web;
        //        clientContext.Load(web);
        //        clientContext.ExecuteQuery();

        //        WebTitle = web.Title;
        //        Console.WriteLine("CSOM | Web title is: {0}", web.Title);
        //    }

        //    Assert.IsNotNull(WebTitle);
        //}
    }
}
