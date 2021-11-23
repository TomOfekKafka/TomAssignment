using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ErmeticServerSideSimulator
{
    public class RequestsHandler
    {
        private readonly ClientsValidationManager _clientsValidationManager;
        public RequestsHandler(ClientsValidationManager clientsValidationManager)
        {
            _clientsValidationManager = clientsValidationManager;
        }

        public HandleRequestResult HandleRequest(HttpRequestEncapsulator encapsulatedRequest)
        {
            var clientId = RetrieveClientIdFromRequest(encapsulatedRequest.Request);
            if (clientId == null) return HandleRequestResult.Create(HttpStatusCode.BadRequest, null, encapsulatedRequest.RequestTimeStamp);
            lock (this)
            {
                IClientRequestsRateWatcher watcher = _clientsValidationManager.GetOrAdd(clientId, encapsulatedRequest.RequestTimeStamp);
                var canProcessRequest = watcher.UpdateAboutRequestAndReturnAvailability(encapsulatedRequest.RequestTimeStamp);
                var httpStatusCode = canProcessRequest ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable;
                return HandleRequestResult.Create(httpStatusCode, clientId, encapsulatedRequest.RequestTimeStamp);
            }
        }

        private string RetrieveClientIdFromRequest(HttpListenerRequest request)
        {
            var clientId = request.QueryString["clientId"];
            return clientId;
        }
    }
}
