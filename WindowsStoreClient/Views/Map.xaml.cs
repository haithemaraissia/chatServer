using Bing.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
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
    public sealed partial class Map : WindowsStoreClient.Common.LayoutAwarePage
    {
        #region "Members"

        MapClient tabletToHandle = new MapClient();
        PushPin mapPushPin = new PushPin();

        #endregion

        #region "Constructor"

        public Map()
        {
            this.InitializeComponent();
            this.UserMap.Center = new Bing.Maps.Location(39.80, -98.55);            
        }

        #endregion

        #region "Event Handlers"

        private async void shareLocationLink_Click(object sender, RoutedEventArgs e)
        {            
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.Default;

            try
            {
                // Read user's location.
                Geoposition geoposition = await geolocator.GetGeopositionAsync();

                // Map it on device.
                this.AddPushPin(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

                tabletToHandle.ClientId = App.Current.DeviceID;
                tabletToHandle.ClientLocation = new MapClient.Location();
                tabletToHandle.ClientLocation.Latitude = geoposition.Coordinate.Latitude;
                tabletToHandle.ClientLocation.Longitude = geoposition.Coordinate.Longitude;

                // Share location with server for mapping.
                App.Current.SignalRHub.MapIt(tabletToHandle);
            }
            catch (Exception ex)
            {
                // Do some error handling here.
            }
        }

        #endregion

        #region "Methods"

        private void AddPushPin(double lat, double lon)
        {
            Location userLoc = new Location(lat, lon);
            this.UserMap.Center = userLoc;
            this.UserMap.ZoomLevel = 10;

            // Remove any previous location icon.
            if (this.UserMap.Children.Count > 0)
            {
                this.UserMap.Children.RemoveAt(0);
            }

            // Add our custom user control pushpin.
            this.UserMap.Children.Add(mapPushPin);
            MapLayer.SetPosition(mapPushPin, userLoc);
        }

        #endregion
    }
}
