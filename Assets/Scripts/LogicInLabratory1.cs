using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicInLabratory1  : MonoBehaviour
{
    public Animator RobotAnimator;
    public Animator DoorAnimator;

    public bool requDoor = false;
    public bool doJump = false;
    private bool cupismoved = false;

    public void setCupIsMoved(bool b)
    {
        cupismoved = b;
    }

    public bool cupIsMoved()
    {
        return cupismoved;
    }

    public void openDoor()
    {
        DoorAnimator.SetBool("opened", true);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (RobotAnimator.GetBool("down") == true)
        {
            //Arm is lowered! 
            requDoor = true;
        }
        else if (RobotAnimator.GetBool("down") == false)
        {
            requDoor = false;
        }
    }*/

    /*void OnTriggerStay(Collider other)
    {
        if(requDoor == true)
        {
            if (RobotAnimator.GetBool("down") == false && cupIsMoved())
            {
                DoorAnimator.SetBool("opened", true);
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        requDoor = false;
    }*/
}
