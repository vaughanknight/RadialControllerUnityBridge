using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace RadialControllerHelper.Events
{
    public class RadialControllerScreenContact
    {
        public Vector2 Position { get; set; }
        public Vector2 Bounds { get; set; }
    }
    
    // Click and rotation delegates
    public delegate void RadialControllerButtonClickedEventHandler(object sender, RadialControllerButtonClickedEventArgs args);
    public delegate void RadialControllerRotationChangedEventHandler(object sender, RadialControllerRotationChangedEventArgs args);

    // Control delegates
    public delegate void RadialControllerControlAcquiredEventHandler(object sender, RadialControllerControlAcquiredEventArgs args);
    public delegate void RadialControllerControlLostEventHandler(object sender, object args);

    // Contact delegates
    public delegate void RadialControllerScreenContactStartedEventHandler(object sender, RadialControllerScreenContactStartedEventArgs args);
    public delegate void RadialControllerScreenContactContinuedEventHandler(object sender, RadialControllerScreenContactContinuedEventArgs args);
    public delegate void RadialControllerScreenContactEndedEventHandler(object sender, object args);
    
    #region Contact Event Args
    public class RadialControllerScreenContactContinuedEventArgs
    {
        public RadialControllerScreenContact Contact { get; set; }
    }

    public class RadialControllerScreenContactStartedEventArgs
    {
        public RadialControllerScreenContact Contact { get; set; }
    }
    #endregion 

    #region Rotation and Click Event Args
    public class RadialControllerRotationChangedEventArgs
    {
        public float RotationDeltaInDegrees { get; set; }
        public RadialControllerScreenContact Contact { get; set; }
    }
    
    public class RadialControllerButtonClickedEventArgs
    {
        public RadialControllerScreenContact Contact { get; set; }
    }
    #endregion

    #region Control Event Args
    public class RadialControllerControlAcquiredEventArgs
    {
        public RadialControllerScreenContact Contact { get; set; }
    }
    #endregion 

}
