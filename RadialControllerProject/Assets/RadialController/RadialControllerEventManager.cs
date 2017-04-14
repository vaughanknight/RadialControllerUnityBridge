using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RadialControllerHelper;
using RadialControllerHelper.Events;
using System;

/// <summary>
/// Gives safe threaded events from the controller.  This gets around 
/// threading issues where the controller events fire outside the Unity threads.
/// Developers can work around this issue, but this class means they don't even have to worry.
/// </summary>
public class RadialControllerEventManager : MonoBehaviour {
    
    private RadialControllerUnityBridge m_controller;
    
    #region Thread Safe Events
    
    // Bools are only used to check events firing 
    // Events expose the thread safe event that gets triggered when the 
    // dial event fires.  

    /// <summary>
    /// When the dial is clicked
    /// </summary>
    public event RadialControllerButtonClickedEventHandler ButtonClicked;
    private RadialControllerButtonClickedEventArgs m_buttonClickedEventArgs;
    private bool m_buttonClickedEventFired = false;

    /// <summary>
    /// Fires when the user selects the application menu item from the radial menu
    /// </summary>
    public event RadialControllerControlAcquiredEventHandler ControlAcquired;
    private RadialControllerControlAcquiredEventArgs m_controlAcquiredEventArgs;
    private bool m_controlAcquiredEventFired = false;

    /// <summary>
    /// Fires when the dial is rotated
    /// </summary>
    public event RadialControllerRotationChangedEventHandler RotationChanged;
    private RadialControllerRotationChangedEventArgs m_rotationChangedEventArgs;
    private bool m_rotationChangedEventFired = false;

    /// <summary>
    /// Fires when the radial controller menu is brought up again
    /// </summary>
    public event RadialControllerControlLostEventHandler ControlLost;
    private object m_controlLostEventArgs;
    private bool m_controlLostEventFired = false;

    /// <summary>
    /// Fires when the dial is lifted off the screen that can detect it
    /// </summary>
    public event RadialControllerScreenContactEndedEventHandler ScreenContactEnded;
    private object m_screenContactEndedEventArgs;
    private bool m_screenContactEndedEventFired = false;

    /// <summary>
    /// When the dial remains on the screen 
    /// </summary>
    public event RadialControllerScreenContactContinuedEventHandler ScreenContactContinued;
    private RadialControllerScreenContactContinuedEventArgs m_screenContactContinuedEventArgs;
    private bool m_screenContactContinuedEventFired = false;

    /// <summary>
    /// When the dial first touches the screen 
    /// </summary>
    public event RadialControllerScreenContactStartedEventHandler ScreenContactStarted;
    private RadialControllerScreenContactStartedEventArgs m_screenContactStartedEventArgs;
    private bool m_screenContactStartedEventFired = false;

    #endregion

    
    // Use this for initialization
    void Start () {
        try { 
            m_controller = RadialControllerUnityBridge.Instance;
        
            m_controller.ButtonClicked += controller_ButtonClicked;
            m_controller.ControlAcquired += controller_ControlAcquired;
            m_controller.ControlLost += controller_ControlLost;
            m_controller.RotationChanged += controller_RotationChanged;
            m_controller.ScreenContactContinued += controller_ScreenContactContinued;
            m_controller.ScreenContactEnded += controller_ScreenContactEnded;
            m_controller.ScreenContactStarted += controller_ScreenContactStarted;
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("Error while starting Radial Controller event handler. {0}", e.ToString());
        }
    }

    private void controller_ScreenContactStarted(object sender, RadialControllerScreenContactStartedEventArgs args)
    {
        m_screenContactStartedEventArgs = args;
        m_screenContactStartedEventFired = true;
    }

    private void controller_ScreenContactEnded(object sender, object args)
    {
        m_screenContactEndedEventArgs = args;
        m_screenContactEndedEventFired = true;
    }

    private void controller_ScreenContactContinued(object sender, RadialControllerScreenContactContinuedEventArgs args)
    {
        m_screenContactContinuedEventArgs = args;
        m_screenContactContinuedEventFired = true;
    }

    private void controller_ControlLost(object sender, object args)
    {
        m_controlLostEventArgs = args;
        m_controlLostEventFired = true;
    }
    
    private void controller_ControlAcquired(object sender, RadialControllerControlAcquiredEventArgs args)
    {
        m_controlAcquiredEventArgs = args;
        m_controlAcquiredEventFired = true;
    }

    private void controller_ButtonClicked(object sender, RadialControllerButtonClickedEventArgs args)
    {
        m_buttonClickedEventArgs = args;
        m_buttonClickedEventFired = true;
    }

    private void controller_RotationChanged(object sender, RadialControllerRotationChangedEventArgs args)
    {
        // At 1 degree this event may fire more than the frame rate
        // so we need to add up the rotation deltas
        if(m_rotationChangedEventFired)
        {
            // Add the rotation, and copy the latest contact information
            m_rotationChangedEventArgs.RotationDeltaInDegrees += args.RotationDeltaInDegrees;
            m_rotationChangedEventArgs.Contact = args.Contact;
        }
        else
        {
            m_rotationChangedEventArgs = args;
            m_rotationChangedEventFired = true;
        }
    }
    
    /// <summary>
    /// Update is where the events all get fired.  Verbose but
    /// performs a low CPU when no events are firing
    /// </summary>
    void Update () {

        if (m_buttonClickedEventFired)
        {
            ButtonClicked(this, m_buttonClickedEventArgs);
            m_buttonClickedEventFired = false;
        }

        if(m_rotationChangedEventFired)
        {
            RotationChanged(this, m_rotationChangedEventArgs);
            m_rotationChangedEventFired = false;
        }

        if (m_screenContactStartedEventFired)
        {
            ScreenContactStarted(this, m_screenContactStartedEventArgs);
            m_screenContactStartedEventFired = false;
        }

        if (m_screenContactContinuedEventFired)
        {
            ScreenContactContinued(this, m_screenContactContinuedEventArgs);
            m_screenContactContinuedEventFired = false;
        }

        if (m_screenContactEndedEventFired)
        {
            ScreenContactEnded(this, m_screenContactEndedEventArgs);
            m_screenContactEndedEventFired = false;
        }

        if (m_controlAcquiredEventFired)
        {
            ControlAcquired(this, m_controlAcquiredEventArgs);
            m_controlAcquiredEventFired = false;
        }

        if (m_controlLostEventFired)
        {
            ControlLost(this, m_controlLostEventArgs);
            m_controlLostEventFired = false;
        }
    }


}
