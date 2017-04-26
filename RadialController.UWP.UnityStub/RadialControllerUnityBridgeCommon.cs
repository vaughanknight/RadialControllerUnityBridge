using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RadialControllerHelper.Events;
using UnityEngine;

namespace RadialControllerHelper
{
    public interface IRadialControllerUnityBridge
    {
        float RotationResolutionInDegrees { get; set; }
        bool UseAutomaticHapticFeedback { get; set; }

        void AddMenuItem(string title, Texture2D icon);
    }


    /// <summary>
    /// This is to pull together the common code.  We can not use the
    /// TypedEventHandler in Unity, so we have customer delegates to take care
    /// of it.  Partials keep it super clean, the interface means you keep needing to 
    /// clean up if it changes.
    /// </summary>
    public partial class RadialControllerUnityBridge : IRadialControllerUnityBridge
    {
        private static RadialControllerUnityBridge _instance;
        public static RadialControllerUnityBridge Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RadialControllerUnityBridge();
                    _instance.Initialise();
                }
                return _instance;
            }
        }

        // Click and rotation        
        public event RadialControllerButtonClickedEventHandler ButtonClicked;
        public event RadialControllerRotationChangedEventHandler RotationChanged;

        // Control
        public event RadialControllerControlAcquiredEventHandler ControlAcquired;
        public event RadialControllerControlLostEventHandler ControlLost;

        // Contact events
        public event RadialControllerScreenContactContinuedEventHandler ScreenContactContinued;
        public event RadialControllerScreenContactEndedEventHandler ScreenContactEnded;
        public event RadialControllerScreenContactStartedEventHandler ScreenContactStarted;
    }
}