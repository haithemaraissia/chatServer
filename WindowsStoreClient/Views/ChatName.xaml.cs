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
    public sealed partial class ChatName : WindowsStoreClient.Common.LayoutAwarePage
    {
        #region "constructor"

        public ChatName()
        {
            this.InitializeComponent();
        }

        #endregion

        #region "Event Handlers"

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTextBox.Text.Trim().Length > 0)
                goButton.IsEnabled = true;
            else
                goButton.IsEnabled = false;
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.ChatUserName = nameTextBox.Text.Trim();
            this.Frame.Navigate(typeof(Chat));
        }

        #endregion
    }
}
