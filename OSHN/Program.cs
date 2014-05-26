using System;
using System.Diagnostics;
using Microsoft.Owin.Hosting;

namespace OSHN
{
    internal class Program
    {
        private static void Main()
        {
            const string url = "http://localhost:9999";

            using (WebApp.Start<Startup>(url))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Host listening on: {0}", url);

                Process.Start(url);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press enter to exit...");

                Console.ReadLine();
            }
        }
    }
}