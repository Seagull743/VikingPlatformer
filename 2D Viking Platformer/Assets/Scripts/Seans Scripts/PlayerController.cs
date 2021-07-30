using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
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
    private Transform groundChecker;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private float groundCheckRadius;
    private bool isGrounded;
    public bool canJump;

    [SerializeField]
    private GameObject PowerMeter;

    Animator anim;

    [SerializeField]
    private Transform collisionDection;

    private BoxCollider2D coll;
    public  bool collidlingLeft = false;
    public bool collidingRight = false;
    public LayerMask lm;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        canJump = true;
        
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        collidlingLeft = CollisionLeft();
        collidingRight = CollisionRight();
    }



    void Update()
    {
        if (!isGrounded)
        {
            anim.SetBool("isjumping", true);
        }
        else if (isGrounded)
        {
            anim.SetBool("isjumping", false);
        }
        

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, ground);
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

        if (Input.GetKeyDown(jump) && isGrounded && canJump)
        {
            anim.SetTrigger("takeoff");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    
        if(rb.velocity.x < 0 && transform.localScale.x >= 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
        else if(rb.velocity.x > 0 && transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
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
        anim.SetBool("Run", false);
    }


    void animateChar()
    {
        if ((rb.velocity.x > 0.1f || rb.velocity.x < -0.1f) && isGrounded)

        {     
            anim.SetBool("Run", true);    
        }
        else
        {        
            anim.SetBool("Run", false);       
        }
        
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

