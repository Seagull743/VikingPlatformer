using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    
    public bool thrown = false;

    private Rigidbody2D rb;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && thrown)
        {
            if(collision.gameObject.GetComponent<MeleeDude>() != null)
            {
                thrown = false;
                collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
                rb.velocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.isKinematic = true;
                Invoke("KinematicToggle", 0.3f);
            }
            else if(collision.gameObject.GetComponent<Archer>() != null)
            {
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
                rb.velocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.isKinematic = true;
                Invoke("KinematicToggle", 0.3f);
            }
        }
        else if (collision.gameObject.tag != "Enemy" && thrown)
        {
            Invoke("ThrownToggle", 1.5f);
        }
    }
    public void KinematicToggle()
    {
        rb.drag = 0;
        rb.isKinematic = false;
        rb.freezeRotation = false;
    }

    public void TurnOff()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ThrownToggle()
    {
        thrown = false;
    }

    public void TurnOn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}


