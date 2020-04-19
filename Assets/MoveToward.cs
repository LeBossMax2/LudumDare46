using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : MonoBehaviour
{
    public float speedFactor;
    public string targetName;
    public bool active = false;

    private GameObject target;

    private Rigidbody2D rb2;

    private bool fire1Active = false;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.Find(targetName);
    }

    public void Activate()
    {
        active = true;
    }

    private void FixedUpdate()
    {
        if (target != null && active)
            rb2.velocity = (target.transform.position - transform.position).normalized * speedFactor;
    }
}
