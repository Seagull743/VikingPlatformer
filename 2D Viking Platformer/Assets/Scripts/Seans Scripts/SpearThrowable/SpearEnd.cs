﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnd : MonoBehaviour
{
    private Spear spear;
    [SerializeField]
    private ParticleSystem IceEffect;
    [SerializeField]
    private Transform spearend;

    private void Start()
    {
        spear = GetComponentInParent<Spear>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
 {
        if (collision.gameObject.tag == "Enemy" && spear.thrown)
        {
            if (collision.gameObject.GetComponent<MeleeDude>() != null)
            {
                spear.HitEnemy();
                collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
            }
            else if (collision.gameObject.GetComponent<Archer>() != null)
            {
                spear.HitEnemy();
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
            }
        }
        else if (collision.gameObject.tag == "IceWall" && spear.thrown)
        {
            spear.IceWallCollision();
            spear.ItemHighLight();
            Instantiate(IceEffect, spearend.position, spearend.rotation);
        }
        else if (collision.gameObject.tag != "Enemy" && spear.thrown && collision.gameObject.tag != "wall" && collision.gameObject.tag != "Box" && collision.gameObject.tag != "IceWallHammer" && collision.gameObject.tag != "Spear" && collision.gameObject.tag != "BreakableIce")
        {
            spear.ThrownToggle();
        }
      
     }

}
