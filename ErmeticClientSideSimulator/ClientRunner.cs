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

        private const int MaxTimeToWaitInMiliSeconds = 1500;

        public ClientRunner()
        {
            _clientIdentifier = Guid.NewGuid().ToString();
            _httpClient = new HttpClient();
        }

        public void Start(LifetimeManager lifetimeManager)
        {
            Task.Run(async() =>
            {
                Thread.CurrentThread.IsBackground = false;
                var random = new Random();
                while (!lifetimeManager.ShouldStop())
                {
                    var response = await _httpClient.GetAsync($"http://localhost:8080?clientId={_clientIdentifier}");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ConsoleDebugAssistant.PrintResponseStatusMessage(response.StatusCode, responseBody);
                    var timeToWait = random.Next() % MaxTimeToWaitInMiliSeconds;
                    await Task.Delay(timeToWait);
                }
                
            });
        }
    }
}
