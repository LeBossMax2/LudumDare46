﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementController : MonoBehaviour
{
    public float speedFactor;
    public float throwSpeed;

    public AudioSource pickupAudio;
    public AudioSource attackAudio;

    private Animator animator;
    private Rigidbody2D rb2;
    private FixedJoint2D fj2;
    private Vector2 velocity = Vector2.zero;
    private Vector2 direction = Vector2.right;

    private float objectTakenMass = 0;
    private GameObject objectTaken;
    public List<GameObject> objectsToTake { get; } = new List<GameObject>();

    private bool fire1Active = false;
    private bool fire2Active = false;

    public void Pickup(GameObject obj)
    {
        objectTaken = obj;
        Rigidbody2D objRB = objectTaken.GetComponent<Rigidbody2D>();
        if (objRB != null)
        {
            objectTakenMass = objRB.mass;
            objRB.mass = 1;
            fj2.connectedBody = objRB;
            fj2.enabled = true;
        }

        animator.SetBool("Hold", true);
        pickupAudio.Play();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        fj2 = GetComponent<FixedJoint2D>();
        fj2.enabled = false;
    }

    private void Update()
    {
        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (velocity.sqrMagnitude >= 1)
            velocity.Normalize();
        
        if (velocity.sqrMagnitude > 0)
            direction = velocity.normalized;

        if (direction.y < -0.7) // DOWN 0
            animator.SetInteger("Direction", 0);
        else if (direction.y > 0.7) // UP 1
            animator.SetInteger("Direction", 1);

        if (direction.x < -0.7) // LEFT 2
            animator.SetInteger("Direction", 2);
        else if (direction.x > 0.7) // RIGHT 3
            animator.SetInteger("Direction", 3);

        animator.SetFloat("Speed", velocity.sqrMagnitude);

        if (Input.GetAxis("Fire1") > 0.5)
        {
            //attack

            if (!fire1Active)
            {
                fire1Active = true;
                if (objectTaken == null)
                {
                    animator.SetTrigger("Attack");
                    attackAudio.Play();
                }
            }
        }
        else
            fire1Active = false;

        if (Input.GetAxis("Fire2") > 0.5)
        {
            //shield / take
            if (!fire2Active)
            {
                fire2Active = true;
                if (objectTaken == null)
                {
                    objectsToTake.RemoveAll(obj => obj == null);
                    if (objectsToTake.Count > 0)
                    {
                        // Take object
                        GameObject obj = objectsToTake.OrderBy(@object => (@object.transform.position - this.transform.position).sqrMagnitude).First();
                        if (obj.GetComponent<Chest>() != null)
                            obj.GetComponent<Chest>().Open(this);
                        else
                            Pickup(obj);
                    }
                }
                else
                {
                    // Drop object
                    objectTaken.transform.parent = transform.parent;

                    Rigidbody2D objRB = objectTaken.GetComponent<Rigidbody2D>();
                    if (objRB != null)
                    {
                        //objRB.simulated = true;
                        objRB.mass = objectTakenMass;
                        objRB.velocity = direction * throwSpeed;
                    }

                    fj2.connectedBody = null;
                    fj2.enabled = false;
                    objectTaken = null;
                    animator.SetBool("Hold", false);
                    pickupAudio.Play();
                }
            }
        }
        else
            fire2Active = false;
    }

    private void FixedUpdate()
    {
        rb2.velocity = velocity * speedFactor;
    }
}
