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

    public void LeverOn()
    {
        anim.SetBool("Pulled", true);
        door.OpenDoor();
    }



}
