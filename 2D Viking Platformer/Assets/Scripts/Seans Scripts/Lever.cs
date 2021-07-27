using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    private bool isOn = false;
    [SerializeField]
    private Door door;


    // Start is called before the first frame update


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void LeverToggle()
    {
        if (!isOn)
        {
            LeverOn();
        }
    }


    public void LeverOn()
    {
        anim.SetBool("Pulled", true);
        isOn = true;
        door.OpenDoor();
    }



}
