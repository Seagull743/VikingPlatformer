using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private Door door;

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
                if (player.GetComponent<Interact>() != null)
                {
                    Interact interact = player.GetComponent<Interact>();
                    if (!interact.pickedUpCrate && !interact.pickedUpPlayer)
                    {
                        LeverOn();
                    }
                }           
            }
        }
    }

    public void LeverOn()
    {
        anim.SetBool("Pulled", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        door.OpenDoor();
    }

}
