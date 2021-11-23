using System;
using System.Net;

namespace ErmeticCommon
{
    public static class ConsoleDebugAssistant
    {
        public static void PrintInfoMessage(string message)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(message);
            }
        }

        public static void PrintResponseStatusMessage(HttpStatusCode statusCode, string message)
        {
            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    foregroundColor = ConsoleColor.Black;
                    backgroundColor = ConsoleColor.Yellow;
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    foregroundColor = ConsoleColor.Black;
                    backgroundColor = ConsoleColor.Red;
                    break;
                case HttpStatusCode.OK:
                    foregroundColor = ConsoleColor.White;
                    backgroundColor = ConsoleColor.Green;
                    break;
                default:
                    foregroundColor = ConsoleColor.Black;
                    backgroundColor = ConsoleColor.White;
                    break;
            }

            lock (Console.Out)
            {
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.WriteLine(message);
            }
        }
    }
}
