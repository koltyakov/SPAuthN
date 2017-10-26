using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPAuthN.Test {
    class Program {
        static void Main(string[] args) {

            string authHeaders = SP.GetAuth();
            Console.WriteLine(authHeaders);

            var bp = "bp"; bp = "";

        }
    }
}
