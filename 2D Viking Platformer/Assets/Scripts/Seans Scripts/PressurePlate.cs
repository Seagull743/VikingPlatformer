using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool opened = false;
    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {

            if (!opened)
            {
                Debug.Log("On");
                opened = true;
            }
            
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            if (opened)
            {
                Debug.Log("Off");
                opened = false;
            }
        }
    }
}
