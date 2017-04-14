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
            Debug.LogError("Creating instance.");
            _radialController = RadialControllerUnityBridge.Instance;
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("Error while Initialising and configuring Radial Controller. {0}", e.ToString());
        }
    }

    public void Start()
    {
        Debug.LogError("Setting rotation.");
        _radialController.RotationResolutionInDegrees = RotationResolution;

        Debug.LogError("Setting haptic.");
        _radialController.UseAutomaticHapticFeedback = UseAutomaticHapticFeedback;

        foreach (var m in MenuItems)
        {
            var data = m.Icon.EncodeToPNG();
            _radialController.AddMenuItem(m.Title, m.Icon);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
