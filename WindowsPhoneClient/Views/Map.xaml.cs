using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Media;
using System.Device.Location;


namespace WindowsPhoneClient.Views
{
    public partial class Map : PhoneApplicationPage
    {
        MapClient phoneToHandle = new MapClient();

        #region "Constructor"

        public Map()
        {
            InitializeComponent();
        }

        #endregion

        #region "Event Handlers"

        private async void shareLocationLink_Click(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                // Read user's location.
                Geoposition geoposition = await geolocator.GetGeopositionAsync();

                // Map it on phone.
                this.AddPushPin(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

                phoneToHandle.ClientId = App.Current.DeviceID;
                phoneToHandle.ClientLocation = new MapClient.Location();
                phoneToHandle.ClientLocation.Latitude = geoposition.Coordinate.Latitude;
                phoneToHandle.ClientLocation.Longitude = geoposition.Coordinate.Longitude;

                // Share location with server for mapping.
                App.Current.SignalRHub.MapIt(phoneToHandle);
            }
            catch (Exception ex)
            {
                // Do some error handling here.
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            try
            {
                // Ask server to unmap.
                App.Current.SignalRHub.UnMapIt(phoneToHandle);
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
            locationMap.Center = new GeoCoordinate(lat, lon);            
            locationMap.ZoomLevel = 10;

            Pushpin pushpin = (Pushpin)this.FindName("MyPushpin");
            pushpin.Visibility = System.Windows.Visibility.Visible;
            pushpin.GeoCoordinate = new GeoCoordinate(lat, lon);          
        }

        #endregion
    }
}