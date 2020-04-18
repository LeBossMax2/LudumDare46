using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Sprite deadSprite;

    public void OnDeath()
    {
        GetComponent<SpriteRenderer>().sprite = deadSprite;
    }
}
