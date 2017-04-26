using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialControlledRotator : MonoBehaviour {

    private Transform _parent;
    private Transform _thisTransform;
    private float _rotation = 0f;
    public float rotationSpeed = 1f;
    private float _startRotationSpeed;
    private float _rotationOffset = 0;

    public RadialControllerEventManager RadialController;

	// Use this for initialization
	void Start () {
        _thisTransform = this.transform;
        _parent = _thisTransform.parent;
        _startRotationSpeed = rotationSpeed;
        RadialController.RotationChanged += RadialController_RotationChanged;	
	}

    private void RadialController_RotationChanged(object sender, RadialControllerHelper.Events.RadialControllerRotationChangedEventArgs args)
    {
        _rotationOffset += args.RotationDeltaInDegrees;
        rotationSpeed = _startRotationSpeed * (1 + _rotationOffset / 360f);
    }

    // Update is called once per frame
    void Update ()
    {
        _rotation = rotationSpeed * Time.deltaTime * Mathf.PI * 2;
        _thisTransform.RotateAround(_parent.position, Vector3.up, _rotation);
	}
}
