using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mist : MonoBehaviour
{

    private float timer = 5f;


    private void Update()
    {
        timer -= Time.deltaTime;
        {
            if(timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                collision.gameObject.GetComponent<Interactive>().StartThrowForce();

            }
        }
    }

}
