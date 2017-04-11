using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Input;
using static RadialControllerHelper.Events.RadialControllerExtensions;

namespace RadialControllerHelper
{
    public partial class RadialControllerUnityBridge : IRadialControllerUnityBridge
    {
        private RadialController _controller;

        /// <summary>
        /// Minimum rotation before receiving events.  
        /// </summary>
        public float RotationResolutionInDegrees
        {
            get { return (float) _controller.RotationResolutionInDegrees;  }
            set { _controller.RotationResolutionInDegrees = value; }
        }

        /// <summary>
        /// Enable haptic feedback to fire when the dial rotates
        /// the minimum RotationResolutionInDegrees
        /// </summary>
        public bool UseAutomaticHapticFeedback
        {
            get { return _controller.UseAutomaticHapticFeedback; }
            set { _controller.UseAutomaticHapticFeedback = value; }
        }

        public async void Initialise()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
                {
                    _controller = RadialController.CreateForCurrentView();
                    
                    wireEvents();
                }
            );
        }

        public async void AddMenuItem(string title, Texture2D icon)
        {
            var memStream = await GetRandomAccessStreamReferenceForIcon(icon);
            var menuItem = RadialControllerMenuItem.CreateFromIcon(title, memStream);

            _controller.Menu.Items.Add(menuItem);
        }

        public static async Task<RandomAccessStreamReference> GetRandomAccessStreamReferenceForIcon(Texture2D icon)
        {
            var bytes = icon.EncodeToPNG();
            var memStream = new MemoryStream(bytes);
            var randStream = await ConvertToRandomAccessStream(memStream);
            return RandomAccessStreamReference.CreateFromStream(randStream);
        }

        public static async Task<IRandomAccessStream> ConvertToRandomAccessStream(MemoryStream memoryStream)
        {
            var randomAccessStream = new InMemoryRandomAccessStream();

            var outputStream = randomAccessStream.GetOutputStreamAt(0);
            var dw = new DataWriter(outputStream);
            var task = new Task(() => dw.WriteBytes(memoryStream.ToArray()));
            task.Start();

            await task;
            await dw.StoreAsync();

            await outputStream.FlushAsync();

            return randomAccessStream;
        }

        /// <summary>
        /// Wires all the events for the controller
        /// </summary>
        private void wireEvents()
        {
            _controller.RotationChanged += _controller_RotationChanged;
            _controller.ButtonClicked += _controller_ButtonClicked;
            _controller.ScreenContactStarted += _controller_ScreenContactStarted;
            _controller.ScreenContactContinued += _controller_ScreenContactContinued;
            _controller.ScreenContactEnded += _controller_ScreenContactEnded;
            _controller.ControlAcquired += _controller_ControlAcquired;
            _controller.ControlLost += _controller_ControlLost;
        }

        private void _controller_ControlLost(RadialController sender, object args)
        {
            if (ControlLost != null)
            {
                ControlLost(null, args);
            }
        }

        private void _controller_ControlAcquired(RadialController sender, RadialControllerControlAcquiredEventArgs args)
        {
            if (ControlAcquired != null)
            {
                ControlAcquired(null, args.ToUnity());
            }
        }

        private void _controller_ScreenContactEnded(RadialController sender, object args)
        {
            if (ScreenContactEnded != null)
            {
                ScreenContactEnded(null, args);
            }
        }

        private void _controller_ScreenContactContinued(RadialController sender, RadialControllerScreenContactContinuedEventArgs args)
        {
            if (ScreenContactContinued != null)
            {
                ScreenContactContinued(null, args.ToUnity());
            }
        }

        private void _controller_ScreenContactStarted(RadialController sender, RadialControllerScreenContactStartedEventArgs args)
        {
            if(ScreenContactStarted != null)
            {
                ScreenContactStarted(null, args.ToUnity());
            }
        }

        private void _controller_ButtonClicked(RadialController sender, RadialControllerButtonClickedEventArgs args)
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(null, args.ToUnity());
            }
        }

        private void _controller_RotationChanged(RadialController sender, Windows.UI.Input.RadialControllerRotationChangedEventArgs args)
        {
            if(RotationChanged != null)
            {
                RotationChanged(null, args.ToUnity());
            }
        }
    }
}
