using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIceWallHammer : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    
   
   public void IceBreak()
    {
        anim.SetTrigger("broken");
    }

    public void DestroyCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

}
