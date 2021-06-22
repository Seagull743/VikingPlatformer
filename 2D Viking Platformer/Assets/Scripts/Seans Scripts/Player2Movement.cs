using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float JumpForce = 5;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
