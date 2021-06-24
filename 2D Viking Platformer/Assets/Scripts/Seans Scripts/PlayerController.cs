using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, ground);


        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
      
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    
        if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-0.3375f, 0.725f, 1);
        }
        else if(rb.velocity.x > 0)
        {
           transform.localScale = new Vector3(0.3375f, 0.725f, 1);
        }
    }


    
}
