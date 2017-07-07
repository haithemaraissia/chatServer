using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneClient
{
    // Custom delegate.
    public delegate void SignalRServerHandler(object sender, SignalREventArgs e);

    public interface ISignalRHub
    {
        // Custom event to act when something happens on SignalR Server.
        event SignalRServerHandler SignalRServerNotification;

        // Operations on SignalR server.
        void MapIt(MapClient phoneClientToMap);
        void UnMapIt(MapClient phoneClientToUnmap);

        void JoinChat(ChatClient phoneChatClient);
        void Chat(string phoneChatMessage);
        void LeaveChat(ChatClient phoneChatClient);

        void FollowLiveGame();

        void StartObjectSync();
        void DoObjectSync(CustomClass objSyncFromPhone);
    }
}
