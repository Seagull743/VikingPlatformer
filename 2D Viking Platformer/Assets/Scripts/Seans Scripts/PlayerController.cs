using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D rb;

    [SerializeField]
    private KeyCode left;
    [SerializeField]
    private KeyCode right;
    [SerializeField]
    private KeyCode jump;

    [SerializeField]
    private Transform groundCheckerLeft;
    [SerializeField]
    private Transform groundCheckerRight;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private float groundCheckRadius;



    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool LeftGrounded;
    [HideInInspector] public bool RightGrounded;
    [HideInInspector] public bool canJump;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isJumping;


    [SerializeField]
    private GameObject PowerMeter;

    private ChangeAnimationStateController stateC;

    [SerializeField]
    private Transform collisionDection;

    private BoxCollider2D coll;
    public bool collidlingLeft = false;
    public bool collidingRight = false;
    public bool collidingUp = false;
    public LayerMask lm;

    private bool touchingIce = false;
    [SerializeField]
    private float iceSpeed = 0.1f; //0.0015 

    public bool facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        canJump = true;
        stateC = GetComponent<ChangeAnimationStateController>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        collidlingLeft = CollisionLeft();
        collidingRight = CollisionRight();
        collidingUp = CollisionTop();
    }

    void Update()
    {
           
        if (!isGrounded)
        {
            isJumping = true;
        }
        else if (isGrounded)
        {
            isJumping = false;
        }
        
        LeftGrounded = Physics2D.OverlapCircle(groundCheckerLeft.position, groundCheckRadius, ground);
        RightGrounded = Physics2D.OverlapCircle(groundCheckerRight.position, groundCheckRadius, ground);

        if(LeftGrounded || RightGrounded)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        animateChar();

        if (isGrounded)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }

        if (Input.GetKey(left) && !collidlingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(right) && !collidingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(jump) && isGrounded && canJump && !collidingUp)
        {
            isJumping = true;
            //anim.SetTrigger("takeoff");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        if(rb.velocity.x < 0 && transform.localScale.x >= 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            facingLeft = true;
        }
        else if(rb.velocity.x > 0 && transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            facingLeft = false;
        }
        //Ice
        if (touchingIce)
        {
            moveSpeed = 0;
            jumpForce = 10;

            if (!facingLeft)
            {
                rb.AddForce(new Vector2(iceSpeed, 0));
                isRunning = false;
            }
            else if (facingLeft)
            {
                rb.AddForce(new Vector2(-iceSpeed, 0));
                isRunning = false;
            }
        }
        else if (!touchingIce)
        {
            touchingIce = false;
            //rb.AddForce(new Vector2(0, 0));
            moveSpeed = 5;
            jumpForce = 13;
        }

    }
    public void controller()
    {
        StartCoroutine(playercontrolsActive());
    }

    IEnumerator playercontrolsActive()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<PlayerController>().enabled = true;
    }

    public void TurnOffAnim()
    {
        //anim.SetBool("Run", false);
        isRunning = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ice")
        {
            touchingIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ice")
        {
            touchingIce = false;
        }
    }

    void animateChar()
    {
        if (isGrounded)
        {
            if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
        }
    }
    private bool CollisionTop()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, 0.10f, lm))
            return true;
        else return false;
    }

    private bool CollisionLeft()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, 0.05f, lm))
            return true;
        else return false;        
    }

    private bool CollisionRight()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, 0.05f, lm))
            return true;
        else return false;

    }
}

