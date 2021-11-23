using System;

namespace ErmeticCommon
{
    public class LifetimeManager
    {
        private bool _shouldStop = false;

        public bool ShouldStop()
        {
            lock (this)
            {
                return _shouldStop;
            }
        }

        public void Stop()
        {
            lock (this)
            {
                _shouldStop = true;
            }
        }
        
    }
}
