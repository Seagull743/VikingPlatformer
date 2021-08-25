using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDude : MonoBehaviour
{
    
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
  
    [HideInInspector]  public Transform target;
    [HideInInspector]  public bool inRange;
    
    public GameObject hotZone;
    public  GameObject triggerArea;

    private Animator anim;
    private float distance;
    private bool attackMode;
    
    private bool cooling;
    private float intTimer;

    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private Transform ground;
    [SerializeField]
    private LayerMask groundLayer;
    private bool isGrounded;
    
    
    private void Awake()
    {
        SelectTarget();
        intTimer = attackTimer;
        isGrounded = Physics2D.OverlapCircle(ground.position, groundCheckRadius, groundLayer);
    }

    
    
    // Update is called once per frame
    void Update()
    {

        if (!attackMode && isGrounded)
        {
            Move();
        }
       
        //need to put !inside anim current anim state etc
        if(!InsideWayPoints() && !inRange && isGrounded)
        {
            SelectTarget();
        }

        if(inRange && isGrounded)
        {
            EnemyLogic();
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


    private bool InsideWayPoints()
    {
        return transform.position.x > waypoint1.position.x && transform.position.x < waypoint2.position.x;
    }


    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, waypoint1.position);
        float distanceToRight = Vector2.Distance(transform.position, waypoint2.position);

        if(distanceToLeft > distanceToRight)
        {
            target = waypoint1;
        }
        else
        {
            target = waypoint2;
        }

        Flip();
    }


    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
