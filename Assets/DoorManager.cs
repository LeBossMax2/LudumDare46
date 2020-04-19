using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class DoorManager : WaveActivable
{
    public bool startOpen = false;
    public Tilemap map;

    public TileBase openTile;
    public string levelToLoad;

    private Vector3Int pos;
    private new BoxCollider2D collider;
    private AudioSource openSound;
    private Light2D light2D;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        openSound = GetComponent<AudioSource>();
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        pos = map.layoutGrid.WorldToCell(transform.position);
        collider.enabled = startOpen;
    }

    public override void Activate()
    {
        map.SetTile(pos, openTile);
        collider.enabled = true;
        openSound.Play();
        light2D.enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
