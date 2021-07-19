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

        if (isGrounded)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }

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
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
}

