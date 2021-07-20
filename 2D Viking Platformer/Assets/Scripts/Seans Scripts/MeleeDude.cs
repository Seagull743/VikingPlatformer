using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDude : MonoBehaviour
{
    [SerializeField]
    private Transform raycast;
    [SerializeField]
    private LayerMask raycastMask;
    [SerializeField]
    private float raycastLength;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackTimer;

    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;


    private void Awake()
    {
        intTimer = attackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);
            RayCastDebugger();
        }

        //Player Detection
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }
    
        if(inRange == false)
        {
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            //turn off attack anim
        }
    }


    private void Move()
    {
        Vector2 targetPostition = new Vector2(target.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPostition, moveSpeed * Time.deltaTime);

        //set anim move to true
        // if (!anim.GetCurrentAnimatorStateInfo(0).IsName("animName"))
        {
            //Vector2 targetPostition = new Vector2(target.transform.position.x, transform.position.y);
            // transform.position = Vector2.MoveTowards(transform.position, targetPostition, moveSpeed * Time.deltaTime);
        }
    }

    void CoolDown()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0 && cooling && attackMode)
        {
            cooling = false;
            attackTimer = intTimer;
        }
    }

    void Attack()
    {
        attackTimer = intTimer;
        attackMode = true;

        //stop walk anim
        //play attack anim
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        //ser attack anim to false
    }


    void RayCastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.green);
        }
    }
}
