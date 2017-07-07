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
    public partial class GameScores : PhoneApplicationPage
    {
        #region "Constructor"

        public GameScores()
        {
            InitializeComponent();
        }

        #endregion

        #region "Event Handlers"

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Wire-up to listen to custom event from SignalR Hub.
            App.Current.SignalRHub.SignalRServerNotification += new SignalRServerHandler(SignalRHub_SignalRServerNotification);

            // Subscribe to server for following game scores.
            App.Current.SignalRHub.FollowLiveGame();
        }

        protected void SignalRHub_SignalRServerNotification(object sender, SignalREventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                // Update scores on phone.
                teamAScore.Text = e.TeamAScore.ToString();
                teamBScore.Text = e.TeamBScore.ToString();
            }));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Unwire.
            App.Current.SignalRHub.SignalRServerNotification -= new SignalRServerHandler(SignalRHub_SignalRServerNotification);
        }

        #endregion
    }
}