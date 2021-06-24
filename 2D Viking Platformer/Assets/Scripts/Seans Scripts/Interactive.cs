
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{

    [SerializeField]
    private Transform grabDetect;
    [SerializeField]
    private Transform holdLocation;
    [SerializeField]
    private float raydist;
    [SerializeField]
    private KeyCode interact;
    [SerializeField]
    private KeyCode throwing;

    private bool isHolding = false;

    [SerializeField]
    private float throwforce;

    private RaycastHit2D grabcheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(interact))
        {
            if (!isHolding)
            {
                grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist);
                if (grabcheck.collider != null && grabcheck.collider.tag == "Box")
                {         
                    grabcheck.collider.gameObject.transform.parent = holdLocation;
                    grabcheck.collider.gameObject.transform.position = holdLocation.position;
                    grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    isHolding = true;
                }           
            }
            else if (isHolding)
            {
                grabcheck.collider.gameObject.transform.parent = null;
                grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                isHolding = false;  
            }
        }
        if (Input.GetKeyDown(throwing) && isHolding)
        {
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwforce;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabcheck.collider.gameObject.transform.parent = null;
        }
    }
}
