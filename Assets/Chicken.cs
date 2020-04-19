using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Sprite deadSprite;
    public AudioSource deathAudio;

    public void OnDeath()
    {
        AudioSource[] src = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in src)
        {
            audio.Stop();
        }

        deathAudio.Play();
        GetComponent<SpriteRenderer>().sprite = deadSprite;
    }
}
