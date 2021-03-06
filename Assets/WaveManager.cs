﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : WaveActivable
{
    [Serializable]
    public struct Wave
    {
        public List<Vector3Int> tileActuators;
        public List<WaveActivable> actuators;
        public float duration;
    }

    public Tilemap map;
    public List<Wave> waves;
    public bool active = true;

    private float timer = 0;
    private int currentWave = -1;

    public override void Activate()
    {
        active = true;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            timer += Time.fixedDeltaTime;

            if (currentWave == -1)
                NextWave();
            else if (currentWave < waves.Count && timer >= waves[currentWave].duration)
                NextWave();
        }
    }

    private void NextWave()
    {
        currentWave++;
        timer = 0;

        if (currentWave < waves.Count)
        {
            foreach (WaveActivable act in waves[currentWave].actuators)
            {
                act?.Activate();
            }
            foreach (Vector3Int actPos in waves[currentWave].tileActuators)
            {
                WaveActivable act = map.GetInstantiatedObject(actPos)?.GetComponentInChildren<WaveActivable>();
                act?.Activate();
            }
        }
    }

}