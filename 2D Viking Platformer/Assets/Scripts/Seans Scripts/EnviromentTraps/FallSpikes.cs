using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpikes : MonoBehaviour
{
    [SerializeField]
    private GameObject spikes;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spikes.GetComponent<Rigidbody2D>().isKinematic = false;
            gameObject.GetComponent<FallSpikes>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
