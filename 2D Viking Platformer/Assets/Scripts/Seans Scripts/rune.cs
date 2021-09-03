using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune : MonoBehaviour
{

    private Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleRuneOn()
    {
        anim.SetBool("isActivated", true);
    }

    public void ToggleRuneOff()
    {
        anim.SetBool("isActivated", false);
    }

}
