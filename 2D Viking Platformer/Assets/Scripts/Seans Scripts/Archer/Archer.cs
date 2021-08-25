using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Rigidbody2D arrow;
    public Transform arrowInstantiate;
    public bool fired = false;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        RaycastHit2D Vision = Physics2D.Raycast(arrowInstantiate.position, Vector2.right * transform.localScale, 3.5f);
        if (Vision.collider != null && Vision.collider.tag == "Player")
        {
            if (!fired)
            {
                anim.SetBool("seen", true);
                fired = true;
            }
            else
            {
                anim.SetBool("seen", false);
            }
        }
    }
    
    private void ArrowFire()
    {
            fired = true;
            Invoke("ArrowCooldown", 1);
            Rigidbody2D ArrowInstance;
            ArrowInstance = Instantiate(arrow, arrowInstantiate.position, arrowInstantiate.rotation) as Rigidbody2D;
            ArrowInstance.AddForce(arrowInstantiate.right * 350f);      
    }

    private void ArrowCooldown()
    {
        fired = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            Destroy(gameObject);
        }
    }
    
}
