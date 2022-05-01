using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{

    public Animator Door;
    AudioSource button;
    // Use this for initialization
    void Start()
    {
        button = GetComponent<AudioSource>();
        button.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("buttonpress");
        button.Play(0);
        Door.SetBool("opened", true);

    }
}
