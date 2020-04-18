using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : WaveActivable
{
    public bool autoActivate = false;
    public float activeTime;
    public float inactiveTime;

    private Animator anim;

    private float activeTimer;
    private float inactiveTimer;

    private bool active;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (!active)
        {
            if (autoActivate)
            {
                inactiveTimer += Time.fixedDeltaTime;

                if (inactiveTimer >= inactiveTime)
                    Activate();
            }
        }
        else
        {
            activeTimer += Time.fixedDeltaTime;

            if (activeTimer >= activeTime)
                Deactivate();
        }
    }

    public override void Activate()
    {
        active = true;
        activeTimer = 0;
        anim.SetTrigger("Start");
        anim.ResetTrigger("Stop");
    }

    private void Deactivate()
    {
        active = false;
        inactiveTimer = 0;
        anim.SetTrigger("Stop");
        anim.ResetTrigger("Start");
    }
}
