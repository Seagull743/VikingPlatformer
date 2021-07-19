using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    private bool isOn = false;
    [SerializeField]
    private GameObject door;

    // Start is called before the first frame update


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
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
        door.SetActive(false);
    }


    public void LeverOff()
    {
        anim.SetBool("Pulled", false);
        isOn = false;
    }

}
