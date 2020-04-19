using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioSource hitAudio;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        hitAudio.Play();
    }
}
