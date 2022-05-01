using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    public Animator robotAnimator;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void doPunch()
    {
        if(robotAnimator.GetBool("extended") == false && robotAnimator.GetBool("grab"))
        {
            robotAnimator.SetTrigger("Punch");
            print("punch");
            StartCoroutine(delayedBoxcast());
        }
    }

    IEnumerator delayedBoxcast()
    {
        yield return new WaitForSeconds(0.5f);
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + Vector3.up * .5f, Vector3.one * .5f, transform.forward);
        foreach (RaycastHit hit in hits)
        {
            print(hit);
            PunchableItem other = hit.collider.GetComponent<PunchableItem>();
            if (other)
            {
                other.GetHit();
            }
        }
    }

    
}


