using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHitBox : MonoBehaviour
{
    [HideInInspector]
    public bool hit;
    [HideInInspector]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {





       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hit = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hit = false;
            player = null;
        }
    }
}
