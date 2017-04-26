using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public float VerticalMovement = 1f;
    public float Speed = 1f;

    private Vector3 _startPos;
    private Transform _thisTransform;
    
	// Use this for initialization
	void Start () {
        _thisTransform = this.transform;
        _startPos = _thisTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
        var yOffset = VerticalMovement * Mathf.Sin(Time.timeSinceLevelLoad * Mathf.PI * 2 / Speed);

        _thisTransform.position = new Vector3(_startPos.x, _startPos.y + yOffset, _startPos.z);
	}
}
