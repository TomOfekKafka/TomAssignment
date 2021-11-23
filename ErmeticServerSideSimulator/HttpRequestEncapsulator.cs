using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace ErmeticServerSideSimulator
{
    public class HttpRequestEncapsulator
    {
        public HttpListenerRequest Request { get; }
        public DateTime RequestTimeStamp { get; }

        public HttpRequestEncapsulator(HttpListenerRequest request, DateTime requestTimeStamp)
        {
            Request = request;
            RequestTimeStamp = requestTimeStamp;
        }
    }
}
