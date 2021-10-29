using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialog DialogCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DialogCanvas.Interaction();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } 
    }



}
