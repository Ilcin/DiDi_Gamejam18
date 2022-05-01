using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Animator animator;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
    }

    public bool doJump()
    {
        if(animator.GetBool("down") && animator.GetBool("grab"))
        {
            animator.SetTrigger("Jump");
            return true;
        }
        return false;
    }
}
