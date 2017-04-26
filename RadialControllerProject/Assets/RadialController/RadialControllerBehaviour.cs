using RadialControllerHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MenuItem
{
    public string Title;
    public Texture2D Icon;
}

public class RadialControllerBehaviour : MonoBehaviour {

    public List<MenuItem> MenuItems = new List<MenuItem>() { new MenuItem() };
    public float RotationResolution = 10f;
    public bool UseAutomaticHapticFeedback = true;

    private RadialControllerUnityBridge _radialController;

    /// <summary>
    /// Invoke the Initialise in the awake method
    /// TODO: Move the Intialise into the bridge, hide it
    /// from Unity completely
    /// </summary>
    void Awake ()
    {
        try
        {
            Debug.Log("Creating RadialControllerUnityBridge Instance.");
            _radialController = RadialControllerUnityBridge.Instance;
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("Error while Initialising and configuring RadialControllerUnityBridge: {0}", e.StackTrace);
        }
    }

    public void Start()
    {
        // RadialController can be in differing states outside of
        // the Unity so we put all the initialisation in a try/catch 
        try {

            _radialController.RotationResolutionInDegrees = RotationResolution;
            _radialController.UseAutomaticHapticFeedback = UseAutomaticHapticFeedback;

            foreach (var m in MenuItems)
            {
                var data = m.Icon.EncodeToPNG();
                _radialController.AddMenuItem(m.Title, m.Icon);
            }
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("Exception creating icons for menu: {0}", e.StackTrace);
        }
    }
}
