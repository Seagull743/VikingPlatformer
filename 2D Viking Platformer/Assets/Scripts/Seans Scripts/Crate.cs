using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem crateBreak;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            Instantiate(crateBreak, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

}
