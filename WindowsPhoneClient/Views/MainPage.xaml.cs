using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneClient.Resources;

namespace WindowsPhoneClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region "Constructor"

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion

        #region "Event Handlers"

        private void mapBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/Map.xaml", UriKind.Relative));
        }

        private void chatBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/ChatName.xaml", UriKind.Relative));
        }

        private void gameBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/GameScores.xaml", UriKind.Relative));
        }

        private void objBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/ObjSync.xaml", UriKind.Relative));
        }

        #endregion
    }
}