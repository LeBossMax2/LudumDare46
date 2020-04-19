using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : WaveActivable
{
    public GameObject prefab;

    public override void Activate()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
