using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace SignalRBackend
{
    public class MapHub : Hub
    {
        // This is in-memory .. persist as per needs.
        private static List<MapClient> ConnectedClientsList = new List<MapClient>();

        public void ShowClientOnMap(MapClient clientToShowOnMap)
        {
            // Add client & broadcast location to all clients connected to this Hub.
            ConnectedClientsList.Add(clientToShowOnMap);
            Clients.All.addMapClient(clientToShowOnMap);
        }

        public void RemoveClientFromMap(MapClient clientToRmeoveFromMap)
        {
            // Clean-up.
            ConnectedClientsList.Remove(clientToRmeoveFromMap);
            Clients.All.removeMapClient(clientToRmeoveFromMap);
        }
    }
}