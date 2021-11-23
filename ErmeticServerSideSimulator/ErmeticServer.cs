using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ErmeticCommon;

namespace ErmeticServerSideSimulator
{
    public class ErmeticServer
    {
        private readonly HttpListener _httpListener;
        private readonly RequestsHandler _requestsHandler;
        private readonly LifetimeManager _serverLifetimeManager;
        public ErmeticServer()
        {
            _httpListener = new HttpListener();
            _requestsHandler = new RequestsHandler(new ClientsValidationManager());
            _serverLifetimeManager = new LifetimeManager();   
        }

        public void Start()
        {
            _httpListener.Prefixes.Add("http://localhost:8080/");
            _httpListener.Start();
            var listenerThread = new Thread(() =>
            {
                while (!_serverLifetimeManager.ShouldStop())
                {
                    try
                    {
                        var asyncResult = _httpListener.BeginGetContext(ListenerCallback, _httpListener);
                        asyncResult.AsyncWaitHandle.WaitOne();
                    }

                    catch (InvalidOperationException)
                    {
                        ConsoleDebugAssistant.PrintInfoMessage("Server is already closed. Can't process");
                    }
                }
            });

            listenerThread.Start();
        }

        public void Stop()
        {
            _serverLifetimeManager.Stop();
            _httpListener.Stop();
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            try
            {
                HttpListenerContext context = listener.EndGetContext(result);
                HandleRequest(context, DateTime.Now);
            }

            catch (HttpListenerException)
            {
                ConsoleDebugAssistant.PrintInfoMessage("Server is already closed. Can't process");
            }

            
        }

        private void HandleRequest(HttpListenerContext context, DateTime requestTime)
        {
            Task.Run(() =>
            {
                Thread.CurrentThread.IsBackground = false; //for server to exit gracefully and wait for all tasks to finish
                var encapsulatedRequest = new HttpRequestEncapsulator(context.Request, requestTime);
                var handleRequestResult = _requestsHandler.HandleRequest(encapsulatedRequest);
                var response = context.Response;
                TransmitResponse(response, handleRequestResult);
            });
        }

        private void TransmitResponse(HttpListenerResponse response, HandleRequestResult handleRequestResult)
        {
            response.StatusCode = (int)handleRequestResult.HttpStatusCode;
            string responseString = $"Status code is {handleRequestResult.HttpStatusCode} for request from client {handleRequestResult.ClientId} at time: {handleRequestResult.RequestTimeStamp:HH:mm:ss:fff}";
            ConsoleDebugAssistant.PrintResponseStatusMessage(handleRequestResult.HttpStatusCode, responseString);
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            try
            {
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }

            catch (ObjectDisposedException)
            {
                Console.WriteLine("Server is already closed. Can't process");
            }
        }
    }
}

