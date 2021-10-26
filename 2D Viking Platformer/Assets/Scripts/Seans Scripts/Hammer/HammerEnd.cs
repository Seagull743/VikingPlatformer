﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerEnd : MonoBehaviour
{

    private Hammer hammer;

    // Start is called before the first frame update
    void Start()
    {
        hammer = GetComponentInParent<Hammer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && hammer.thrown)
        {
            Debug.Log("Hit");
            
            if (collision.gameObject.GetComponent<MeleeDude>() != null)
            {
   
            }
            else if (collision.gameObject.GetComponent<Archer>() != null)
            {
                Debug.Log("I hit Skeleton");
                hammer.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
            }
        }
        else if(collision.gameObject.tag == "IceWallHammer" && hammer.thrown)
        {
            collision.gameObject.GetComponent<BreakIceWallHammer>().IceBreak();
            hammer.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            hammer.stopAnimation();
            hammer.Invoke("ThrownToggle", 2.5f);
        }
        else if (collision.gameObject.tag != "Enemy" && hammer.thrown && collision.gameObject.tag != "wall" && collision.gameObject.tag != "Box")
        {
            hammer.stopAnimation();
            //hammer.Invoke("ThrownToggle", 2.5f);
        }

    }

}