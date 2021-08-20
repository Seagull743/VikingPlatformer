using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableIcePlatform : MonoBehaviour
{
   
    private float timer;

    private bool startTimer = false;

    private Animator anim;
   
    void Start()
    {
        anim = GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= 3)
            {
                timer = 3;
                anim.SetTrigger("broken"); 
            }
        }
        else if(!startTimer)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            if(timer == 0)
            {
                timer = 0;
            }
            }           
        }   
    }
      
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            startTimer = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            startTimer = false;
        }
    }

    void BoxColliderbreak()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }


}
