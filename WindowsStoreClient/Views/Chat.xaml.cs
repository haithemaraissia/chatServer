using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace WindowsStoreClient.Views
{    
    public sealed partial class Chat : WindowsStoreClient.Common.LayoutAwarePage
    {
        #region "Members"

        ChatClient tabletToChat = new ChatClient();

        #endregion

        #region "Constructor"

        public Chat()
        {
            this.InitializeComponent();
            tabletToChat.ClientId = App.Current.DeviceID;
            tabletToChat.ChatUserName = App.Current.ChatUserName;
        }

        #endregion

        #region "Event Handlers"

        private void sendChatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (chatTextbox.Text.Trim() != string.Empty)
            {
                string tabletChatMessage = chatTextbox.Text.Trim();
                chatTextbox.Text = string.Empty;

                // Send to server chatroom.
                App.Current.SignalRHub.Chat(tabletChatMessage);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Send to server for joining Chatroom.
            App.Current.SignalRHub.JoinChat(tabletToChat);

            // Wire-up to listen to custom event from SignalR Hub.
            App.Current.SignalRHub.SignalRServerNotification += new SignalRServerHandler(SignalRHub_SignalRServerNotification);
        }

        protected async void SignalRHub_SignalRServerNotification(object sender, SignalREventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Add to local ChatRoom.
                chatDialog.Text += "\r\n" + e.ChatMessageFromServer;
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Unwire.
            App.Current.SignalRHub.SignalRServerNotification -= new SignalRServerHandler(SignalRHub_SignalRServerNotification);

            // Let server know about leaving Chatroom.
            App.Current.SignalRHub.LeaveChat(tabletToChat);
        }

        #endregion
    }
}
