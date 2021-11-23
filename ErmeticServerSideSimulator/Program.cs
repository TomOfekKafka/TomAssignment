using System;

namespace ErmeticServerSideSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var ermeticServer = new ErmeticServer();
            ermeticServer.Start();
            Console.ReadKey(true);
            ermeticServer.Stop();
        }
    }
}
