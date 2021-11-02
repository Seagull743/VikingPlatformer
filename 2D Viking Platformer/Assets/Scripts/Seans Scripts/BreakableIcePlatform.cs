using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableIcePlatform : MonoBehaviour
{

    private bool start = false;
    private bool broken = false;
    public bool platformDestroyed = false;
    private Animator anim;
    [SerializeField]
    private GameObject IceCollider;
   
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if(start)
        {       
            anim.SetBool("broken", true);
        }
    }
      
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            start = true;
            if(start)
            {
                anim.enabled = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(start && !broken)
            anim.enabled = false;
        }
    }

    void BreakingPlatformstage1()
    {
        broken = true;
        anim.enabled = true;
    }

    void BoxColliderbreak()
    {
        platformDestroyed = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        IceCollider.GetComponent<BoxCollider2D>().enabled = false;

    }

    void DestroyIce()
    {
        platformDestroyed = true;
        gameObject.SetActive(false);
    }

    public void RespawnIce()
    {
        gameObject.SetActive(true);
        start = false;
        anim.SetBool("broken", false);
        broken = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        IceCollider.GetComponent<BoxCollider2D>().enabled = true;
        platformDestroyed = false;
    }
}
