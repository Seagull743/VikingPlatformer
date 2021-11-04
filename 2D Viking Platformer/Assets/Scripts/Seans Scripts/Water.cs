using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem WaterSplash;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform feet = collision.gameObject.GetComponent<PlayerHealth>().BifrostLocation;
            Instantiate(WaterSplash, feet.position, feet.transform.rotation);
        }
    }
}
