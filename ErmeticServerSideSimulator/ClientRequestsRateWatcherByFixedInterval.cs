using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ErmeticServerSideSimulator
{
    public class ClientRequestsRateWatcherByFixedInterval : IClientRequestsRateWatcher
    {
        private TimeSpan _requestsTimeFrame = TimeSpan.FromSeconds(5);
        private const int NumOfAllowedRequestsForTimeFrame = 5;
        
        private DateTime _currentStartTimeOfTimeFrame;
        private int _numOfRequestsReceivedForCurrentTimeFrame;

        public ClientRequestsRateWatcherByFixedInterval(DateTime initialTimeStamp)
        {
            _currentStartTimeOfTimeFrame = initialTimeStamp;
            _numOfRequestsReceivedForCurrentTimeFrame = 1;
        }

        public bool UpdateAboutRequestAndReturnAvailability(DateTime requestTime)
        {
            if (requestTime - _currentStartTimeOfTimeFrame >= _requestsTimeFrame)
            {
                _currentStartTimeOfTimeFrame = requestTime;
                _numOfRequestsReceivedForCurrentTimeFrame = 1;
                return true;
            }

            _numOfRequestsReceivedForCurrentTimeFrame++;
            if (_numOfRequestsReceivedForCurrentTimeFrame > NumOfAllowedRequestsForTimeFrame)
            {
                return false;
            }

            return true;
        }
    }
}
