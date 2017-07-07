using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsStoreClient
{
    // Custom delegate.
    public delegate void SignalRServerHandler(object sender, SignalREventArgs e);

    public interface ISignalRHub
    {
        // Custom event to act when something happens on SignalR Server.
        event SignalRServerHandler SignalRServerNotification;

        // Operations on SignalR server.
        void MapIt(MapClient tabletClientToMap);
        void UnMapIt(MapClient tabletClientToUnmap);

        void JoinChat(ChatClient tabletChatClient);
        void Chat(string tabletChatMessage);
        void LeaveChat(ChatClient tabletChatClient);

        void FollowLiveGame();

        void StartObjectSync();
        void DoObjectSync(CustomClass objSyncFromTablet);
    }
}
