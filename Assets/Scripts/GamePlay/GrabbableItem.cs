using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableItem : MonoBehaviour {

    Vector3 originalPos;
    Quaternion originalRot;
    Collider coll;

	// Use this for initialization
	void Start () {
        originalPos = transform.position;
        originalRot = transform.rotation;
        coll = GetComponent<Collider>();
        coll.enabled = true;
    }
	
    public void returnToOriginalPosition()
    {
        coll.enabled = true;
        transform.position = originalPos;
        transform.rotation = originalRot;
        transform.SetParent(null);
    }
}
