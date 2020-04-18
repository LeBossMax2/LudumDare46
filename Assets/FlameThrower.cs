using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
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
            inactiveTimer += Time.fixedDeltaTime;

            if (inactiveTimer >= inactiveTime)
            {
                inactiveTimer -= inactiveTime;
                Activate();
            }
        }
        else
        {
            activeTimer += Time.fixedDeltaTime;

            if (activeTimer >= activeTime)
            {
                activeTimer -= activeTime;
                Deactivate();
            }
        }
    }

    private void Activate()
    {
        active = true;
        anim.SetTrigger("Start");
        anim.ResetTrigger("Stop");
    }

    private void Deactivate()
    {
        active = false;
        anim.SetTrigger("Stop");
        anim.ResetTrigger("Start");
    }
}
