using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool thrown = false;
    public bool canKill = false;
    private Rigidbody2D rb;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject itemHighlight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   private void Update()
   {
       if (thrown)
       {
           anim.SetBool("thrown", true);
       }
       else if(!thrown)
       {
           anim.SetBool("thrown", false);
       }
   }

    public void HitEnemy()
    {
        thrown = false;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
    }

    public void ThrownToggle()
    {
        thrown = false;
    }

    public void stopAnimation()
    {
        anim.SetBool("thrown", false);
    }

    public void CanStunHammerToggle()
    {
        canKill = false;
    }

    public void TurnOff()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        itemHighlight.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        itemHighlight.SetActive(true);
    }
}
