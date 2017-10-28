using System;

namespace SPAuthN.Test {

    class Program {
        static void Main(string[] args) {

            Headers authHeaders = SPAuth.GetAuth();
            Console.WriteLine(authHeaders);

            var bp = "bp"; bp = "";

        }
    }

}
