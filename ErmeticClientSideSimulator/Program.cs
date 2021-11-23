using System;
using System.Reflection.Metadata.Ecma335;
using ErmeticCommon;

namespace ErmeticClientSideSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter number of clients to simulate:");
            var numberOfClientsToSimulateInput = Console.ReadLine();
            if (!int.TryParse(numberOfClientsToSimulateInput, out var numberOfClientsToSimulate)) return;

            var lifetimeManager = new LifetimeManager();
            for (int i = 0; i < numberOfClientsToSimulate; i++)
            {
                var clientRunner = new ClientRunner();
                clientRunner.Start(lifetimeManager);
            }

            Console.ReadKey(true);
        }
    }
}
