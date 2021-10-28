using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private ParticleSystem trail;


    private void Start()
    {
        trail = GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            trail.Stop();
            other.gameObject.GetComponent<PlayerHealth>().PlayerDamaged();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag != "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            trail.Stop();
            Invoke("WaitDestroy", 0.5f);
        }
    }

    public void WaitDestroy()
    {
        Destroy(gameObject);
    }

}
