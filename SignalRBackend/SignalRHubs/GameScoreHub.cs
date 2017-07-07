using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRBackend
{
    public class GameScoreHub : Hub
    {
        public void PushScoresToClients(GameScoreClient teamScores)
        {
            // Broadcast to all.
            Clients.All.pushScores(teamScores.TeamAScore, teamScores.TeamBScore);
        }    
    }
}