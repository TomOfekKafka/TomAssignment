using System;
using System.Collections.Generic;
using System.Text;

namespace ErmeticServerSideSimulator
{
    public class ClientsValidationManager
    {
        private readonly Dictionary<string, IClientRequestsRateWatcher> _clientToWatcher;

        public ClientsValidationManager()
        {
            _clientToWatcher = new Dictionary<string, IClientRequestsRateWatcher>();
        }

        public IClientRequestsRateWatcher GetOrAdd(string clientId, DateTime initialTimeStamp)
        {
            if (_clientToWatcher.TryGetValue(clientId, out IClientRequestsRateWatcher clientWatcher))
            {
                return clientWatcher;
            }
            var clientBehaviorWatcher = new ClientRequestsRateWatcherByFixedInterval(initialTimeStamp);
            _clientToWatcher[clientId] = clientBehaviorWatcher;
            return clientBehaviorWatcher;
        }

    }
}
