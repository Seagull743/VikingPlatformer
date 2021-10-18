using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public bool thrown = false;
    private Rigidbody2D rb;
    private BoxCollider2D[] bc;
    private float turnspeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponents<BoxCollider2D>();
    }


    private void Update()
    {
        if (rb.velocity.x < 0 && thrown) // facingleft
        {
            transform.Rotate(new Vector3(0f, 0f, 27f * Time.deltaTime));
            Debug.Log("thrownleft");
        }
        else if (rb.velocity.x > 0 && thrown) //facingRight
        {
            transform.Rotate(new Vector3(0f, 0f, -27f * Time.deltaTime));
            Debug.Log("thrownRight");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && thrown)
        {
            if (collision.gameObject.GetComponent<MeleeDude>() != null)
            {
                thrown = false;
                collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
                rb.velocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.isKinematic = true;
                Invoke("KinematicToggle", 0.3f);
                //rb.transform.position = collision.gameObject.transform.position;
            }
            else if (collision.gameObject.GetComponent<Archer>() != null)
            {
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
                rb.velocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.isKinematic = true;
                Invoke("KinematicToggle", 0.3f);
                //rb.transform.position = collision.gameObject.transform.position;
            }
        }
        else if (collision.gameObject.tag == "IceWall" && thrown)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            gameObject.layer = 8;
            foreach(BoxCollider2D bc in bc)
            {
                bc.isTrigger = false;
            }
        }
        else if (collision.gameObject.tag != "Enemy" && thrown)
        {
            thrown = false;
            //Invoke("ThrownToggle", 1.5f);
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
