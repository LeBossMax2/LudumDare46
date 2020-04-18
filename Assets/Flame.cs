using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public bool destroyOnEnd = false;

    public void OnAnimationEnd()
    {
        if (destroyOnEnd)
            Destroy(this.gameObject);
    }
}
