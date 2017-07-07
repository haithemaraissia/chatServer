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
    public sealed partial class ObjSync : WindowsStoreClient.Common.LayoutAwarePage
    {
        #region "Members"

        CustomClass tabletObjToSync = new CustomClass();

        #endregion

        #region "Constructor"

        public ObjSync()
        {
            this.InitializeComponent();
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

        protected async void SignalRHub_SignalRServerNotification(object sender, SignalREventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update object properties on phone.
                prop1Txt.Text = e.CustomObject.Property1.ToString();
                prop2Txt.Text = e.CustomObject.Property2.ToString();
                prop3Chk.IsChecked = e.CustomObject.Property3;
            });
        }

        private void syncBtn_Click(object sender, RoutedEventArgs e)
        {
            tabletObjToSync.Property1 = prop1Txt.Text.Trim();
            tabletObjToSync.Property2 = Convert.ToInt32(prop2Txt.Text.Trim());
            tabletObjToSync.Property3 = (bool)prop3Chk.IsChecked;

            // Send to server to do object sync.
            App.Current.SignalRHub.DoObjectSync(tabletObjToSync);
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
