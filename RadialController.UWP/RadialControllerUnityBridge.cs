using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RadialControllerHelper.Events;

namespace RadialControllerHelper
{
    public partial class RadialControllerUnityBridge : IRadialControllerUnityBridge
    {
        public float RotationResolutionInDegrees { get; set; }
        public bool UseAutomaticHapticFeedback { get; set; }

        public void AddMenuItem(string title, Texture2D icon)
        {
            Debug.Log("Add Menu Item");
            // Test the icon can be converted and is read/write enabled
            // if it fails it will log errors in the editor to save
            // finding out in the Build
            var testBytes = icon.EncodeToPNG();
        }

        public void Initialise()
        {
            Debug.Log("Unity Editor Stub, this message should not occur in UWP build.");
        }
    }
}
