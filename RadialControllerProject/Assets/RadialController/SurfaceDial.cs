using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This behaviour is for attaching to the demo Surface Dial 
/// object
/// </summary>
public class SurfaceDial : MonoBehaviour {

    /// <summary>
    /// A reference to the RadialControllerEvent Manager that triggers
    /// Unity thread safe event calls for the RadialController/SurfaceDial
    /// </summary>
    public RadialControllerEventManager RadialController;
    
	void Start () {

        // NOTE: The event manager ensures all these events are thread safe
        // that being, they will also trigger in the Update method to avoid input
        // thread issues
        RadialController.ButtonClicked += RadialController_ButtonClicked;
        RadialController.RotationChanged += RadialController_RotationChanged;
        RadialController.ScreenContactContinued += RadialController_ScreenContactContinued;
        RadialController.ScreenContactStarted += RadialController_ScreenContactStarted;
        RadialController.ScreenContactEnded += RadialController_ScreenContactEnded;
        RadialController.ControlAcquired += RadialController_ControlAcquired;
        RadialController.ControlLost += RadialController_ControlLost;
    }

    private void RadialController_ControlLost(object sender, object args)
    {
        iTween.ScaleBy(this.gameObject, new Vector3(1/1.2f, 1 / 1.2f, 1 / 1.2f), 0.8f);
    }

    private void RadialController_ControlAcquired(object sender, RadialControllerHelper.Events.RadialControllerControlAcquiredEventArgs args)
    {
        iTween.ScaleBy(this.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.8f);
    }

    private void RadialController_ScreenContactEnded(object sender, object args)
    {
        
    }

    private void RadialController_ScreenContactStarted(object sender, RadialControllerHelper.Events.RadialControllerScreenContactStartedEventArgs args)
    {
        
    }

    private void RadialController_ScreenContactContinued(object sender, RadialControllerHelper.Events.RadialControllerScreenContactContinuedEventArgs args)
    {
        
    }

    private void RadialController_RotationChanged(object sender, RadialControllerHelper.Events.RadialControllerRotationChangedEventArgs args)
    {
        this.transform.Rotate(Vector3.up, args.RotationDeltaInDegrees); 
    }

    public Vector3 ClickPunchScale = new Vector3(0f, 0.2f, 0f);
    public float ClickPunchDuration = 0.4f;

#if UNITY_EDITOR
    public void OnMouseDown()
    {
        ClickPunch();
    }
#endif
    private void RadialController_ButtonClicked(object sender, RadialControllerHelper.Events.RadialControllerButtonClickedEventArgs args)
    {
        ClickPunch();
    }

    private void ClickPunch()
    {
        iTween.PunchScale(this.gameObject, ClickPunchScale, ClickPunchDuration);
    }

    // Update is called once per frame
    void Update () {
        
	}
}
