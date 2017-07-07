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
    public partial class ObjSync : PhoneApplicationPage
    {
        #region "Members"

        CustomClass phoneObjToSync = new CustomClass();

        #endregion

        #region "Constructor"

        public ObjSync()
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

            // Indicate desired object sync to server.
            App.Current.SignalRHub.StartObjectSync();    
        }

        protected void SignalRHub_SignalRServerNotification(object sender, SignalREventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                // Update object properties on phone.
                prop1Txt.Text = e.CustomObject.Property1.ToString();
                prop2Txt.Text = e.CustomObject.Property2.ToString();
                prop3Chk.IsChecked = e.CustomObject.Property3;
            }));
        }

        private void syncBtn_Click(object sender, EventArgs e)
        {                        
            phoneObjToSync.Property1 = prop1Txt.Text.Trim();
            phoneObjToSync.Property2 = Convert.ToInt32(prop2Txt.Text.Trim());
            phoneObjToSync.Property3 = (bool)prop3Chk.IsChecked;

            // Send to server to do object sync.
            App.Current.SignalRHub.DoObjectSync(phoneObjToSync);
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