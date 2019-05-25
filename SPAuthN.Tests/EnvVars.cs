using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SPAuthN.Tests
{
    [TestClass]
    public class EnvVars
    {
        [TestMethod]
        public void EnvVarsNotEmpty()
        {
            string SiteUrl = Environment.GetEnvironmentVariable("SPAUTH_SITEURL");
            string Username = Environment.GetEnvironmentVariable("SPAUTH_USERNAME");
            string Password = Environment.GetEnvironmentVariable("SPAUTH_PASSWORD");         

            Assert.IsNotNull(SiteUrl);
            Assert.IsNotNull(Username);
            Assert.IsNotNull(Password);
        }
    }
}
