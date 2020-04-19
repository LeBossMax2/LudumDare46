using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite openSprite;
    public GameObject obj;
    public WaveManager managerToActivate;

    private bool open = false;

    public bool Open(MovementController player)
    {
        if (open)
            return false;

        GetComponent<SpriteRenderer>().sprite = openSprite;
        this.open = true;

        managerToActivate.Activate();
        obj.SetActive(true);
        player.Pickup(obj);

        return true;
    }
}
