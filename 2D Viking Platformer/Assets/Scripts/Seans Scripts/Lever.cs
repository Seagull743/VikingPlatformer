using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    private bool isOn = false;


    // Start is called before the first frame update


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }



    void Start()
    {
       
    }


    public void LeverToggle()
    {
        if (isOn)
        {
            LeverOff();
        }
        else if (!isOn)
        {
            LeverOn();
        }
    }


    public void LeverOn()
    {
        anim.SetBool("Pulled", true);
        isOn = true;
    }


    public void LeverOff()
    {
        anim.SetBool("Pulled", false);
        isOn = false;
    }

}
