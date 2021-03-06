using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ErmeticCommon;

namespace ErmeticClientSideSimulator
{
    public class ClientRunner
    {
        private readonly string _clientIdentifier;
        private readonly HttpClient _httpClient;

        private const int MaxTimeToWaitInMilliSeconds = 1500;

        public ClientRunner()
        {
            _clientIdentifier = Guid.NewGuid().ToString();
            _httpClient = new HttpClient();
        }

        public void Start(LifetimeManager lifetimeManager)
        {
            Task.Run(async() =>
            {
                Thread.CurrentThread.IsBackground = false; //for client to exit gracefully and wait for all tasks to finish
                var random = new Random();
                while (!lifetimeManager.ShouldStop())
                {
                    var response = await _httpClient.GetAsync($"http://localhost:8080?clientId={_clientIdentifier}");
                    string responseStr = await response.Content.ReadAsStringAsync();
                    ConsoleDebugAssistant.PrintResponseStatusMessage(response.StatusCode, responseStr);
                    var timeToWait = random.Next() % MaxTimeToWaitInMilliSeconds;
                    await Task.Delay(timeToWait);
                }
                
            });
        }
    }
}
