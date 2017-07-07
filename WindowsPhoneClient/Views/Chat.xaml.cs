using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WindowsPhoneClient.Views
{
    public partial class Chat : PhoneApplicationPage
    {
        #region "Members"

        ChatClient phoneToChat = new ChatClient();        

        #endregion

        #region "Constructor"

        public Chat()
        {
            InitializeComponent();
            phoneToChat.ClientId = App.Current.DeviceID;
            phoneToChat.ChatUserName = App.Current.ChatUserName;
        }

        #endregion

        #region "Event Handlers"

        private void sendChatBtn_Click(object sender, EventArgs e)
        {
            if (chatTextbox.Text.Trim() != string.Empty)
            {
                string phoneChatMessage = chatTextbox.Text.Trim();
                chatTextbox.Text = string.Empty;

                // Send to server chatroom.
                App.Current.SignalRHub.Chat(phoneChatMessage);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);            

            // Send to server for joining Chatroom.
            App.Current.SignalRHub.JoinChat(phoneToChat);

            // Wire-up to listen to custom event from SignalR Hub.
            App.Current.SignalRHub.SignalRServerNotification += new SignalRServerHandler(SignalRHub_SignalRServerNotification);
        }

        protected void SignalRHub_SignalRServerNotification(object sender, SignalREventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                // Add to local ChatRoom.
                chatDialog.Text += "\r\n" + e.ChatMessageFromServer;
            }));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Unwire.
            App.Current.SignalRHub.SignalRServerNotification -= new SignalRServerHandler(SignalRHub_SignalRServerNotification);

            // Let server know about leaving Chatroom.
            App.Current.SignalRHub.LeaveChat(phoneToChat);
        }

        #endregion
    }
}