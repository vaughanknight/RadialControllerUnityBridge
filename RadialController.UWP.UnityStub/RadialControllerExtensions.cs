using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RadialControllerHelper.Events
{
    /// <summary>
    /// Extension methods hide the underlying classes in Unity
    /// and increase reuse/simplicity of the Unity classes.
    /// 
    /// Just call 'ToUnity' and it will convert to the Unity available
    /// class of the same name.
    /// </summary>
    public static class RadialControllerExtensions
    {
        /// <summary>
        /// Convert the Point to a Vector2 in Unity
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Windows.Foundation.Point point)
        {
            return new Vector2((float)point.X, (float)point.Y);
        }

        /// <summary>
        /// Converts a Contact to a Unity accessible version.
        /// TODO: Add Bounds
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static RadialControllerScreenContact ToUnity(this Windows.UI.Input.RadialControllerScreenContact contact)
        {
            return new RadialControllerScreenContact()
            {
                Position = contact.Position.ToVector2()
            };
        }

        public static RadialControllerRotationChangedEventArgs ToUnity(this Windows.UI.Input.RadialControllerRotationChangedEventArgs args)
        {
            return new RadialControllerRotationChangedEventArgs()
            {
                Contact = args.Contact?.ToUnity(),
                RotationDeltaInDegrees = (float)args.RotationDeltaInDegrees
            };
        }

        public static RadialControllerControlAcquiredEventArgs ToUnity(this Windows.UI.Input.RadialControllerControlAcquiredEventArgs args)
        {
            return new RadialControllerControlAcquiredEventArgs()
            {
                Contact = args.Contact?.ToUnity(),
            };
        }

        public static RadialControllerButtonClickedEventArgs ToUnity(this Windows.UI.Input.RadialControllerButtonClickedEventArgs args)
        {
            return new RadialControllerButtonClickedEventArgs()
            {
                Contact = args.Contact?.ToUnity(),
            };
        }

        public static RadialControllerScreenContactStartedEventArgs ToUnity(this Windows.UI.Input.RadialControllerScreenContactStartedEventArgs args)
        {
            return new RadialControllerScreenContactStartedEventArgs()
            {
                Contact = args.Contact.ToUnity()
            };
        }
        public static RadialControllerScreenContactContinuedEventArgs ToUnity(this Windows.UI.Input.RadialControllerScreenContactContinuedEventArgs args)
        {
            return new RadialControllerScreenContactContinuedEventArgs()
            {
                Contact = args.Contact?.ToUnity(),
            };
        }
    }
}
