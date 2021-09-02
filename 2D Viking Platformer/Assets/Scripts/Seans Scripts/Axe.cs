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

    // Update is called once per frame
    void Update()
    {
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
                rb.transform.position = collision.gameObject.transform.position;
            }
            else if(collision.gameObject.GetComponent<Archer>() != null)
            {
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
                rb.velocity = Vector3.zero;              
                rb.transform.position = collision.gameObject.transform.position;
            }
        }
        else if (collision.gameObject && collision.gameObject.tag != "Enemy" && thrown)
        {
            thrown = false;
        }
    }

    public void TurnOff()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TurnOn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}


