using System;

namespace SPAuthN.Test
{

    class Program
    {
        static void Main(string[] args)
        {

            Headers authHeaders = SPAuth.GetAuth("--configPath='./config/private.json'");
            Console.WriteLine(authHeaders.Cookie);

            var bp = "bp"; bp = "";

        }
    }

}
