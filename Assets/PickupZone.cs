using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private MovementController player;

    private void Awake()
    {
        player = GetComponentInParent<MovementController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.objectsToTake.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.objectsToTake.Remove(collision.gameObject);
    }
}
