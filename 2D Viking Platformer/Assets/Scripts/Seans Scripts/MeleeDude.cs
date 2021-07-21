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
    [SerializeField]
    private Transform waypoint1;
    [SerializeField]
    private Transform waypoint2;

    private RaycastHit2D hit;
    private Transform target;
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

        if (!attackMode)
        {
            Move();
        }
       
        //need to put !inside anim current anim state etc
        if(!InsideWayPoints() && !inRange)
        {
            SelectTarget();
        }


        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.right, raycastLength, raycastMask);
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
            target = other.transform;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
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
        Vector2 targetPostition = new Vector2(target.position.x, transform.position.y);
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
            Debug.DrawRay(raycast.position, Vector2.right * raycastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, Vector2.right * raycastLength, Color.green);
        }
    }

    private bool InsideWayPoints()
    {
        return transform.position.x > waypoint1.position.x && transform.position.x < waypoint2.position.x;
    }


    void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, waypoint1.position);
    }
}
