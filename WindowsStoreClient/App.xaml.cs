using WindowsStoreClient.Common;
using WindowsStoreClient.Views;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace WindowsStoreClient
{  
    sealed partial class App : Application
    {
        #region "Members"

        public string DeviceID { get; set; }
        public string ChatUserName { get; set; }
        public ISignalRHub SignalRHub { get; set; }

        public static new App Current
        {
            get { return Application.Current as App; }
        }

        #endregion

        #region "Constructor"

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        #endregion

        #region "Event Handlers"

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Set this once for unique Device ID throughout app.
            DeviceID = this.GetHardwareId();

            // Instantiate with default or any other implementation.
            App.Current.SignalRHub = new SignalRMessagingHub();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage)))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        #endregion

        #region "Methods"

        public string GetHardwareId()
        {
            var _Token = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);
            var _Id = _Token.Id;
            var _Reader = Windows.Storage.Streams.DataReader.FromBuffer(_Id);
            var _Bytes = new byte[_Id.Length];
            _Reader.ReadBytes(_Bytes);
            return BitConverter.ToString(_Bytes);
        }

        #endregion
    }
}
