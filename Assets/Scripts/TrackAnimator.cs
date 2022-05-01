using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAnimator : MonoBehaviour {

    public Animator animator;
    public GameObject[] tracks;
    private Material tracksR;
    private Material tracksL;

    public Transform BigWheelR;
    public Transform BigWheelL;

    public Transform SmallWheelR;
    public Transform SmallWheelL;

    public float bigWheelSpeed =500.0f;
    public float smallWheelSpeed = 500.0f;

    private float trackSpeedR =.1f;
    private float trackSpeedL =.1f;

    Vector2 offsetR = Vector2.zero;
    Vector2 offsetL = Vector2.zero;


    // Use this for initialization
    void Start () {

            tracksL = tracks[0].GetComponent<Renderer>().material;
            tracksR = tracks[1].GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update () {
        int moveDir = animator.GetInteger("moveValue");
        if(animator.GetBool("moving") == false)
        {
            trackSpeedL =0.0f;
            trackSpeedR =0.0f;
        }
        else if(moveDir==1)
        {
            trackSpeedL = 2.0f;
            trackSpeedR = 2.0f;
        }
        else if(moveDir==-1)
        {
            trackSpeedL = 2.0f;
            trackSpeedR = 2.0f;
        }
        else if(moveDir == 90)
        {
            trackSpeedL = -2.0f;
            trackSpeedR = 2.0f;
        }
        else if (moveDir == -90)
        {
            trackSpeedL = 2.0f;
            trackSpeedR = -2.0f;
        }
        offsetR.y = (offsetR.y - trackSpeedR*Time.deltaTime);
        tracksR.mainTextureOffset = offsetR;
        offsetL.y = (offsetL.y - trackSpeedL * Time.deltaTime) ;
        tracksL.mainTextureOffset= offsetL;

        BigWheelR.Rotate(Vector3.forward, trackSpeedR * bigWheelSpeed * Time.deltaTime,Space.Self);
        BigWheelL.Rotate(Vector3.forward, trackSpeedL * bigWheelSpeed * Time.deltaTime, Space.Self);

        SmallWheelL.Rotate(Vector3.forward, trackSpeedL * smallWheelSpeed * Time.deltaTime, Space.Self);
        SmallWheelR.Rotate(Vector3.forward, trackSpeedR * smallWheelSpeed * Time.deltaTime,  Space.Self);
        //SmallWheelL.Rotate(Vector3.forward, 5000 * smallWheelSpeed * Time.deltaTime, Space.Self);
        //offsetR.y = (offset.y - trackSpeed) % 1;
        //tracks.SetTextureOffset(1, offset);
    }
}
