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
            Debug.Log("AddMenuItem");
        }

        public void Initialise()
        {
            Debug.LogError("Unity Editor Stub, this message should not occur in UWP build.");
        }
    }
}
