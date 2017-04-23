using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : MonoBehaviour
{
    public Transform Target;
    private Transform _thisTransform;
    public float Speed = 0.1f;
    public Transform Destination;

	// Use this for initialization
	void Start () {
        _thisTransform = this.transform;
        
        iTween.LookTo(gameObject, iTween.Hash("looktarget", Target.transform.position,
            "time", 4f,
            "oncomplete", "OnLookComplete",
//            "delay", 2f,
            "easetype", iTween.EaseType.easeInOutQuart));
    }

    public void OnLookComplete()
    {
        _Track = true;
        iTween.MoveTo(gameObject, 
            iTween.Hash("position", Destination.transform.position,
                        "time", 4f,
                        "easetype", iTween.EaseType.easeInOutQuart));
    }

    private bool _Track = false;
    
    // Update is called once per frame
    void Update () {
        
        
    }

    void LateUpdate()
    {
        if (_Track)
        {
            _thisTransform.LookAt(Target);
        }
    }
}
