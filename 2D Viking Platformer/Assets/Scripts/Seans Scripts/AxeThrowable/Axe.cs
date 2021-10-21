using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    
    public bool thrown = false;
    private Rigidbody2D rb;
    [SerializeField]
    private Animator anim;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (thrown)
        {
            gameObject.layer = 15;
            anim.SetBool("thrown", true);
        }
        else
        {
            gameObject.layer = 21;
            anim.SetBool("thrown", false);
        }
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy" && thrown)
    //    {
    //        if (collision.gameObject.GetComponent<MeleeDude>() != null)
    //        {
    //            collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
    //            thrown = false;
    //            rb.velocity = Vector3.zero;
    //            rb.freezeRotation = true;
    //            rb.isKinematic = true;
    //            Invoke("KinematicToggle", 0.3f);
    //        }
    //        else if (collision.gameObject.GetComponent<Archer>() != null)
    //        {
    //            collision.gameObject.GetComponent<Archer>().EnemyDieing();
    //            thrown = false;
    //            rb.velocity = Vector3.zero;
    //            rb.freezeRotation = true;
    //            rb.isKinematic = true;
    //            Invoke("KinematicToggle", 0.3f);
    //        }
    //    }
    //    else if (collision.gameObject.tag != "Enemy" && thrown)
    //    {
    //        thrown = false;
    //        //Invoke("ThrownToggle", 0.5f);
    //    }
    //}

    public void KinematicToggle()
    {
        rb.drag = 0;
        rb.isKinematic = false;
        rb.freezeRotation = false;
    }

    public void HitEnemy()
    {
        thrown = false;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        rb.isKinematic = true;
        Invoke("KinematicToggle", 0.3f);
    }

    public void ThrownToggle()
    {
        thrown = false;
        rb.isKinematic = true;
        rb.freezeRotation = true;
        rb.velocity = Vector3.zero;
    }


    public void stopAnimation()
    {
        anim.SetBool("thrown", false);
    }

    public void TurnOff()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }


    public void FreezeRotation()
    {
        rb.freezeRotation = true;
    }

    public void AxeStop()
    {
        thrown = false;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        rb.isKinematic = true;
        Invoke("KinematicToggle", 0.3f);
    }

    public void TurnOn()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}


