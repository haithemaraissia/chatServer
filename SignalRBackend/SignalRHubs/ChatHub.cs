using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRBackend
{
    public class ChatHub : Hub
    {
        // This is in-memory .. persist as per needs.
        private static List<ChatClient> ConnectedClientsList = new List<ChatClient>();

        public void JoinChatroom(ChatClient newChatClient)
        {
            // Add client & broadcast new chat user message to all clients connected to this Hub.
            ConnectedClientsList.Add(newChatClient);
            Clients.All.addChatMessage(newChatClient.ChatUserName + " has joined the Chatroom!");

            // Organizing clients in groups ..
            Groups.Add(Context.ConnectionId, "ChatRoom A");
        }

        public void LeaveChatRoom(ChatClient chatClientToRemove)
        {
            // Clean-up.
            ConnectedClientsList.Remove(chatClientToRemove);
            Clients.All.addChatMessage(chatClientToRemove.ChatUserName + " has left the Chatroom!");

            Groups.Remove(Context.ConnectionId, "ChatRoom A");
        }
        
        public void PushChatMessageToClients(string message)
        {
            // Push to all connected clients.
            Clients.All.addChatMessage(message);

            // Communicate to a Group.
            // Clients.Group("ChatRoom A").addChatMessage(message);
            
            // Invoke a method on the calling client only.
            // Clients.Caller.addChatMessage(message);

            // Similar to above, the more verbose way.
            // Clients.Client(Context.ConnectionId).addChatMessage(message);            
        }
    }
}