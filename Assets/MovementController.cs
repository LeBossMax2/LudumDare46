﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    public float speedFactor;
    public float throwSpeed;

    private Rigidbody2D rb2;
    private FixedJoint2D fj2;
    private Vector2 velocity = Vector2.zero;
    private Vector2 direction = Vector2.right;

    private float objectTakenMass = 0;
    private GameObject objectTaken;
    private List<GameObject> objectsToTake = new List<GameObject>();

    private bool fire1Active = false;

    private void Awake()
    {
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

        if (animator != null)
        {
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
            animator.SetFloat("Speed", velocity.sqrMagnitude);
        }

        if (Input.GetAxis("Fire1") > 0.5)
        {
            //attack / take

            if (!fire1Active)
            {
                fire1Active = true;
                if (objectTaken == null)
                {
                    if (objectsToTake.Count > 0)
                    {
                        // Take object
                        objectTaken = objectsToTake.OrderBy(@object => (@object.transform.position - this.transform.position).sqrMagnitude).First();


                        Rigidbody2D objRB = objectTaken.GetComponent<Rigidbody2D>();
                        if (objRB != null)
                        {
                            objectTakenMass = objRB.mass;
                            objRB.mass = 1;
                            fj2.connectedBody = objRB;
                            fj2.enabled = true;
                        }

                        //objectTaken.transform.parent = objectTakeParent;
                        //objectTaken.transform.localPosition = Vector3.zero;
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
                }
            }
        }
        else
            fire1Active = false;

        if (Input.GetAxis("Fire2") > 0.5)
        {
            //shield
        }
    }

    private void FixedUpdate()
    {
        rb2.velocity = velocity * speedFactor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsToTake.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsToTake.Remove(collision.gameObject);
    }
}