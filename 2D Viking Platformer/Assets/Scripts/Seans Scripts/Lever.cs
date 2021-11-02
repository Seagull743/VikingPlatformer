using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private Door door;


    // Start is called before the first frame update


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl))
            {
                LeverOn();
            }
        }
    }


    public void LeverOn()
    {
        anim.SetBool("Pulled", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        door.OpenDoor();
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
      //  {          
      //      LeverOn();
    //    }
  //  }

}
