using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillTarget : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollider(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollider(other.gameObject);
    }

    private void OnCollider(GameObject obj)
    {
        if (obj.CompareTag("target"))
        {
            Chicken ch = obj.GetComponent<Chicken>();

            if (ch != null)
                ch.OnDeath();

            Camera cam = Camera.main;
            cam.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, cam.transform.position.z);
            cam.orthographicSize = 4;
            Time.timeScale = 0;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
    }
}
