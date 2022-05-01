using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : PunchableItem
{

    public Animator animator;
    public EndSequence endseq;
    private AudioSource oof;
    private AudioSource crash;
    private AudioSource[] all;
    public void Start()
    {
        all = GetComponents<AudioSource>();
        if (all[0].clip.name.Equals("Roblox-death-sound"))
        {
            crash = all[1];
            oof = all[0];
        }
        else
        {
            crash = all[0];
            oof = all[1];
        }
        oof.Stop();
        crash.Stop();
    }
    public override void GetHit()
    {
        animator.SetTrigger("Fall");
        crash.PlayDelayed(1.25f);
        oof.PlayDelayed(1.4f);
        endseq.StartEndSequence();
    }
}
