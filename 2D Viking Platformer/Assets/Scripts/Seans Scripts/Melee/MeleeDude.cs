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
  
     public Transform target;
     public bool inRange;
    
    public GameObject hotZone;
    public  GameObject triggerArea;

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

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private BoxCollider2D enemyBody;

    private GameObject player;
    
    //public GameObject AxeBox;

    private bool hit;

    private void Awake()
    {
        SelectTarget();
        intTimer = attackTimer;
        isGrounded = Physics2D.OverlapCircle(ground.position, groundCheckRadius, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        float WaypointDistance = Vector2.Distance(waypoint1.position, waypoint2.position);

        if (!isGrounded)
        {
            SelectTarget();
        }

        if (!attackMode && isGrounded)
        {
            Move();
        }
       
        //need to put !inside anim current anim state etc
        if(!InsideWayPoints() && !inRange && isGrounded && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Melee attack v5"))
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
            anim.SetBool("Attack", false);
        }
    }

    private void Move()
    {
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Melee attack v5"))
        {
            Vector2 targetPostition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPostition, moveSpeed * Time.deltaTime);
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
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hit = true;
            player = other.gameObject; 
        }
    }

    private void DamagePlayer()
    {
        if (hit)
        {
            Debug.Log("HitPlayer");
            player.GetComponent<PlayerHealth>().PlayerDamaged();
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hit = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            anim.SetBool("Death", true);
            moveSpeed = 0;
            enemyBody.enabled = false;
        }
    }

    private void TriggerCooling()
    {
        cooling = true;
    }
    

    public void EnemyDieing()
    {
        moveSpeed = 0;
        anim.SetBool("Death", true);
        enemyBody.enabled = false;
    }

    public void KillEnemy()
    {
        moveSpeed = 0;
        enemyBody.enabled = false;
    }

}
