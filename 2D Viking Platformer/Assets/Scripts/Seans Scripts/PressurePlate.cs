using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool opened = false;
    [SerializeField]
    private Animator anim;

    private void Start()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
     {
         if (!opened)
         {
             opened = true;
             anim.SetBool("isActivated", true);
         }      
     }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (opened)
        { 
            opened = false;
            anim.SetBool("isActivated", true);
        }
    }
}
