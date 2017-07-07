using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WindowsStoreClient.Views
{   
    public sealed partial class MainPage : WindowsStoreClient.Common.LayoutAwarePage
    {
        #region "Constructor"

        public MainPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region "Event Handlers"

        private void mapBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Map));
        }

        private void chatBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChatName));
        }

        private void gameBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GameScores));
        }

        private void objBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ObjSync));
        }

        #endregion
    }
}
