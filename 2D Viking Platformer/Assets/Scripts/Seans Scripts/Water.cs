using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem WaterSplash;


  //private void OnCollisionEnter2D(Collision2D collision)
  //{
  //    if (collision.gameObject.tag == "Player")
  //    {
  //        Transform feet = collision.gameObject.GetComponent<PlayerHealth>().BifrostLocation;
  //        Instantiate(WaterSplash, feet.position, feet.transform.rotation);
  //        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
  //        player.PlayerDrown();
  //        player.Invoke("ResetGame", 3f);
  //    }
  //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform feet = collision.gameObject.GetComponent<PlayerHealth>().BifrostLocation;
            Instantiate(WaterSplash, feet.position, feet.transform.rotation);
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRB.velocity = Vector3.zero;
            player.PlayerDrown();
            //player.Invoke("ResetGame", 2f);
        }
    }

}
