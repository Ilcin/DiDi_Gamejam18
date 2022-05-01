using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour {

    public LogicInLabratory1 lab1Script;
    public Animator anim;
    public float angle = 50.0f;
    GameObject grabbableObject;
    public GameObject grabbedObject;
    public GameObject placeObject;
    public GameObject arm;
    [SerializeField]
    public Vector3 offset;
    [SerializeField]
    public Vector3 rotation;

	// Use this for initialization
	void Start () {
        grabbedObject = null;
	}

    public void grab()
    {
        if (anim.GetBool("extended") && !anim.GetBool("down") && grabbableObject != null && grabbedObject == null)
        {
            //http://answers.unity.com/answers/503942/view.html
            Vector3 f = gameObject.transform.forward;
            f.y = grabbableObject.transform.position.y;
            if (Vector3.Angle(f, (grabbableObject.transform.position - gameObject.transform.position)) < angle)
            {
                grabbedObject = grabbableObject;
                grabbableObject.GetComponent<Collider>().enabled = false;
                grabbableObject = null;
                grabbedObject.transform.SetParent(arm.transform);
                grabbedObject.transform.localPosition = offset;
                grabbedObject.transform.localRotation = Quaternion.Euler(rotation);
            }
        }
    }

    public void ungrab()
    {
        if (!anim.GetBool("grab") && grabbedObject != null)
        {
            if (placeObject == null)
            {
                grabbedObject.GetComponent<GrabbableItem>().returnToOriginalPosition();
                grabbedObject = null;
            }
            else 
            {
                Vector3 f = gameObject.transform.forward;
                f.y = placeObject.transform.position.y;
             
                if (Vector3.Angle(f, (placeObject.transform.position - gameObject.transform.position)) < angle && grabbedObject.name.Equals("Cup"))
                {
                        placeObject.GetComponent<LayDownSpot>().setObject(grabbedObject);
                        grabbedObject = null;
                    //lab1Script.setCupIsMoved(true);
                    lab1Script.openDoor();
                }
                else
                {
                    grabbedObject.GetComponent<GrabbableItem>().returnToOriginalPosition();
                    grabbedObject = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "grabbable")
        {
            grabbableObject = other.gameObject;
        }

        if(other.tag == "layDown")
        {
            placeObject = other.gameObject;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "grabbable")
            grabbableObject = null;
        if (other.tag == "layDown")
            placeObject = null;
    }
}
