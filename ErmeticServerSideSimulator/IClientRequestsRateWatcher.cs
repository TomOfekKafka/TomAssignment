using System;
using System.Collections.Generic;
using System.Text;

namespace ErmeticServerSideSimulator
{
    public interface IClientRequestsRateWatcher
    {
        public bool UpdateAboutRequestAndReturnAvailability(DateTime requestTime);
    }
}
