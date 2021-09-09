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

    private List<GameObject> CollisionItems = new List<GameObject>();



    private void Update()
    {
        if(CollisionItems.Count >= 1)
        {
            if (!opened)
            {
                opened = true;
                anim.SetBool("isActivated", true);
                runeobject.ToggleRuneOn();
                DP.OpenDoor();
            }
        }
        else if(CollisionItems.Count <= 0)
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            CollisionItems.Add(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            CollisionItems.Remove(other.gameObject);
        }    
    }


    
}
