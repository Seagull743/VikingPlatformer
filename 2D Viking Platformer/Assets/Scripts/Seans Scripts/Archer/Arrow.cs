using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().PlayerDamaged();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag != "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("WaitDestroy", 2.5f);
        }
    }

    public void WaitDestroy()
    {
        Destroy(gameObject);
    }

}
