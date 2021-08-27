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
        if (thrown)
        {
            transform.Rotate(0, 0, 1);
        }
        else if(!thrown)
        {
            transform.Rotate(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject && collision.gameObject.tag != "Enemy" && thrown)
        {
            thrown = false;
        }
        else if(collision.gameObject.tag == "Enemy" && thrown)
        {
            collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
            rb.velocity = Vector3.zero;
            rb.position = collision.gameObject.transform.position;
            
        }
    }
}
