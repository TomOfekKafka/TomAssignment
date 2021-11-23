using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ErmeticServerSideSimulator
{
    public class HandleRequestResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ClientId { get; }
        public DateTime RequestTimeStamp { get; }

        private HandleRequestResult(HttpStatusCode httpStatusCode, string clientId, DateTime requestTimeStamp)
        {
            HttpStatusCode = httpStatusCode;
            ClientId = clientId;
            RequestTimeStamp = requestTimeStamp;
        }

        public static HandleRequestResult Create(HttpStatusCode httpStatusCode, string clientId, DateTime requestTimeStamp)
        {
            return new HandleRequestResult(httpStatusCode, clientId, requestTimeStamp);
        }
    }
}
