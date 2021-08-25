using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 5f);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
