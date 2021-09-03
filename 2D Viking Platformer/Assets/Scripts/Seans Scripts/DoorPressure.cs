using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPressure : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
    }



   public void OpenDoor()
    {
        anim.SetBool("isOpen", true);
    }

   public void CloseDoor()
    {
        anim.SetBool("isOpen", false);
    }

}
