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
}
