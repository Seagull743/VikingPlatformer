using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool opened = false;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private rune runeobject;
    [SerializeField]
    private DoorPressure DP;

    private void Start()
    {
    
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            if (!opened)
            {
                opened = true;
                anim.SetBool("isActivated", true);
                runeobject.ToggleRuneOn();
                DP.OpenDoor();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (opened)
        {
            opened = false;
            anim.SetBool("isActivated", false);
            runeobject.ToggleRuneOff();
            DP.CloseDoor();
        }
    }


    
}
