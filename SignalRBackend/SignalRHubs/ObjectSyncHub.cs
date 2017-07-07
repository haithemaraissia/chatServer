using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRBackend
{
    public class ObjectSyncHub : Hub
    {
        public void SyncCustomObjectWithClients(CustomClass customObject)
        {
            Clients.All.syncObject(customObject);
        }
    }
}