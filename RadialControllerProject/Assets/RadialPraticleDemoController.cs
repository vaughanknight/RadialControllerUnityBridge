using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialPraticleDemoController : MonoBehaviour {

    public RadialControllerEventManager RadialController;
    public ParticleSystem Particles;
    
	void Start ()
    {
        RadialController.RotationChanged += RadialController_RotationChanged;
	}

    private void RadialController_RotationChanged(object sender, RadialControllerHelper.Events.RadialControllerRotationChangedEventArgs args)
    {
        UpdateParticles(args.RotationDeltaInDegrees);
    }

    private void UpdateParticles(float rateChange)
    {
        // Grab the struct reference so we can set the value
        var emission = Particles.emission;

        // Change the rate, but keep it over 0
        var rate = emission.rateOverTimeMultiplier;
        rate += rateChange;
        rate = Mathf.Max(rate, 0);
       
        // Set the rate in the particles system
        emission.rateOverTimeMultiplier = rate;
    }

    // Update is called once per frame
    void Update () {

	}
}
