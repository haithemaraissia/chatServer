using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace WindowsStoreClient
{
    public class SignalRMessagingHub : ISignalRHub
    {
        #region "Members"

        IHubProxy SignalRMapHub;
        IHubProxy SignalRChatHub;
        IHubProxy SignalRGameScoreHub;
        IHubProxy SignalRObjSyncHub;

        // Use the specific port# for local server or actual URI if SignalR backend is hosted.        
        HubConnection mapConnection = new HubConnection("http://localhost:53478");
        HubConnection chatConnection = new HubConnection("http://localhost:53478/");
        HubConnection gameConnection = new HubConnection("http://localhost:53478/");
        HubConnection objConnection = new HubConnection("http://localhost:53478/");

        public event SignalRServerHandler SignalRServerNotification;

        #endregion

        #region "Constructor"

        public SignalRMessagingHub()
        {
            // Reference to SignalR Server Hub & Proxy.                      
            SignalRMapHub = mapConnection.CreateHubProxy("MapHub");
            SignalRChatHub = chatConnection.CreateHubProxy("ChatHub");
            SignalRGameScoreHub = gameConnection.CreateHubProxy("GameScoreHub");
            SignalRObjSyncHub = objConnection.CreateHubProxy("ObjectSyncHub");
        }

        #endregion

        #region "Implementation"

        public async virtual void MapIt(MapClient tabletToMap)
        {
            // Fire up SignalR Connection & share location.  
            try
            {
                await mapConnection.Start();

                if (mapConnection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {
                    await SignalRMapHub.Invoke("ShowClientOnMap", tabletToMap);
                }
            }
            catch (Exception)
            {
                // Do some error handling.
            }
        }

        public async virtual void UnMapIt(MapClient tabletToUnMap)
        {
            // Disconnect from Server & unmap.
            SignalRMapHub.Invoke("RemoveClientFromMap", tabletToUnMap);
        }

        public async virtual void JoinChat(ChatClient tabletChatClient)
        {
            // Fire up SignalR Connection & join chatroom.  
            try
            {
                await chatConnection.Start();

                if (chatConnection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {
                    await SignalRChatHub.Invoke("JoinChatroom", tabletChatClient);
                }
            }
            catch (Exception)
            {
                // Do some error handling.
            }

            // Listen to chat events on SignalR Server & wire them up appropriately.
            SignalRChatHub.On<string>("addChatMessage", message =>
            {
                SignalREventArgs chatArgs = new SignalREventArgs();
                chatArgs.ChatMessageFromServer = message;

                // Raise custom event & let it bubble up.
                SignalRServerNotification(this, chatArgs);
            });
        }

        public async virtual void Chat(string tabletChatMessage)
        {
            // Post message to Server Chatroom.
            await SignalRChatHub.Invoke("PushChatMessageToClients", tabletChatMessage);
        }

        public async virtual void LeaveChat(ChatClient tabletChatClient)
        {
            // Leave the Server's Chatroom.
            await SignalRChatHub.Invoke("LeaveChatRoom", tabletChatClient);
        }

        public async virtual void FollowLiveGame()
        {
            try
            {
                await gameConnection.Start();
            }
            catch (Exception)
            {
                // Do some error handling.
            }

            // Listen to Game score updates from SignalR Server & wire them up appropriately.
            SignalRGameScoreHub.On<int, int>("pushScores", (teamAScore, teamBScore) =>
            {
                SignalREventArgs gameScoreArgs = new SignalREventArgs();
                gameScoreArgs.TeamAScore = teamAScore;
                gameScoreArgs.TeamBScore = teamBScore;

                // Raise custom event & let it bubble up.
                SignalRServerNotification(this, gameScoreArgs);
            });
        }

        public async virtual void StartObjectSync()
        {
            // Fire up SignalR Connection & start object sync.  
            try
            {
                await objConnection.Start();
            }
            catch (Exception)
            {
                // Do some error handling.
            }

            // Listen to object updates from SignalR Server & wire them up appropriately.
            SignalRObjSyncHub.On<CustomClass>("syncObject", customObject =>
            {
                SignalREventArgs objSyncArgs = new SignalREventArgs();
                objSyncArgs.CustomObject = customObject;

                // Raise custom event & let it bubble up.
                SignalRServerNotification(this, objSyncArgs);
            });
        }

        public async virtual void DoObjectSync(CustomClass objSyncFromTablet)
        {
            // Post message to Server for object sync from phone.
            await SignalRObjSyncHub.Invoke("SyncCustomObjectWithClients", objSyncFromTablet);
        }

        #endregion

        #region "Methods"

        public virtual void OnSignalRServerNotificationReceived(SignalREventArgs e)
        {
            if (SignalRServerNotification != null)
            {
                SignalRServerNotification(this, e);
            }
        }

        #endregion
    }
}
