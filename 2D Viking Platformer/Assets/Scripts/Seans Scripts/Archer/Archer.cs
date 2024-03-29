﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Rigidbody2D arrow;
    public Transform arrowInstantiate;
    public bool fired = false;
    private Animator anim;
    private BoxCollider2D SkeleCollider;
    [SerializeField]
    private float SkeletonRange = 3.5f;
    private float NormalRange;
    [SerializeField]
    private LayerMask Camera;
    [SerializeField]
    private GameObject StunPartical;
    void Start()
    {
        StunPartical.SetActive(false);
        NormalRange = SkeletonRange;
        anim = GetComponent<Animator>();
        SkeleCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        RaycastHit2D Vision = Physics2D.Raycast(arrowInstantiate.position, Vector2.right * transform.localScale, SkeletonRange, ~Camera);
        Debug.DrawRay(arrowInstantiate.position, Vector2.right * transform.localScale, Color.green, SkeletonRange);
        if (Vision.collider != null && Vision.collider.tag == "Player")
        {
            if (!fired)
            {
                anim.SetBool("seen", true);
                fired = true;
            }
            else
            {
                anim.SetBool("seen", false);
            }
        }
    }
    
    private void ArrowFire()
    {
            fired = true;
            Invoke("ArrowCooldown", 1.3f);
            Rigidbody2D ArrowInstance;
            ArrowInstance = Instantiate(arrow, arrowInstantiate.position, arrowInstantiate.rotation) as Rigidbody2D;
            ArrowInstance.AddForce(arrowInstantiate.right * 350f);      
    }

    private void ArrowCooldown()
    {
        fired = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDieing()
    {
        anim.SetBool("Death", true);
        SkeleCollider.enabled = false;
        fired = true;
        Invoke("KillEnemy", 2.5f);
    }
        
    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    public void Stunned()
    {
        SkeletonRange = 0;
        StunPartical.SetActive(true);
        fired = false;
        anim.enabled = false;
        Invoke("UnStun", 6f);
    }

    public void UnStun()
    {
        StunPartical.SetActive(false);
        SkeletonRange = NormalRange;
        anim.enabled = true;
        fired = false;
    }
}
