using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    GameObject trap;
    public GameObject player;
    public Animator trapAnim;
    public Animator pAnima;
    private Coroutine c;
    // Use this for initialization
    void Start()
    {
        trap = GameObject.FindGameObjectWithTag("trap");
        //player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (pAnima.GetBool("down") && player.transform.position.x > -1 && player.transform.position.x < 0f && player.transform.position.z < -1.58f && player.transform.position.z > -3f && player.transform.rotation.eulerAngles.y >= 225 && player.transform.rotation.eulerAngles.y <= 315)
        {
            if (c == null)
            {
                //todo bewegung ausführen
                //SHRINK LOWER RAISE
                Debug.Log("Coroutien1");
                c = StartCoroutine(Bla());
            }


        }
        else
        {
            if (c != null)
            {
                check();
                StopCoroutine(c);
                c = null;
            }

        }
      
    }
    IEnumerator Bla()
    {
        
        Debug.Log("In IE");
            
        while (check()) {
            
            yield return null;
        
        }
 
    }

    bool check()
    {
        if (!Input.GetKeyDown(KeyCode.R))
            return true;
        trapAnim.SetBool("opened", true);
        return false;
    }




}

