using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayDownSpot : MonoBehaviour {

    [SerializeField]
    public Vector3 offset;
    GameObject objectPlaced;
    public float angle = 50.0f;

    // Use this for initialization
    void Start () {
        objectPlaced = null;
    }

    public void setObject(GameObject obj)
    {
        //set the object
        objectPlaced = obj;
        objectPlaced.transform.parent = null;
        objectPlaced.transform.position = gameObject.transform.position + offset;
        objectPlaced.transform.rotation = Quaternion.identity;
        Destroy(gameObject);
    }
}
