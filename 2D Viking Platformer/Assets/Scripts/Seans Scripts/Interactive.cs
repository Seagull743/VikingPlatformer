
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactive : MonoBehaviour
{

    [SerializeField]
    private Transform holdCheck;
    [SerializeField]
    private Transform grabDetect;
    [SerializeField]
    private Transform placeChecker;
    [SerializeField]
    private Transform holdLocation;
    [SerializeField]
    private Transform dropLocation;
    [SerializeField]
    private Transform dropPlayer;
    [SerializeField]
    private float raydist;
    [SerializeField]
    private KeyCode interact;
    [SerializeField]
    private KeyCode throwing;
    public bool isHolding = false;


    private bool isthrowing = false;

    [SerializeField]
    private float throwforce;

    private RaycastHit2D grabcheck;

    //throwing
    [SerializeField]
    private float holdDownTime;
    [SerializeField]
    private Slider PowerCanvas;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private float Offset;
    private Animator anim;

    [SerializeField]
    private GameObject crate;

    //private Animation anima;

    void Start()
    {
        //anima = GetComponent<Animation>();
        anim = GetComponent<Animator>();
        PowerCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isHolding)
        {
            anim.SetBool("isholding", false);
            anim.SetBool("throwing", false);
            anim.SetBool("thrown", false);
            isthrowing = false;
        }
        else
        {
            anim.SetBool("isholding", true);
        }


        if (isthrowing)
        {
            anim.SetBool("throwing", true);
        }

        PowerCanvas.gameObject.transform.position = this.gameObject.transform.position + Vector3.up * Offset;

        if (Input.GetKeyDown(interact))
        {
            if (!isHolding)
            {
               
                grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist);
                //box
                if (grabcheck.collider != null && grabcheck.collider.tag == "Box")
                {
                    {                    
                        StartCoroutine(InteractStop());
                        isHolding = true;
                        this.gameObject.GetComponent<PlayerController>().canJump = false;              
                    }                 
                }
                //player
                else if (grabcheck.collider != null && grabcheck.collider.tag == "Player")
                {
                    if(grabcheck.collider.gameObject.GetComponent<Interactive>().isHolding == false)
                    {
                        isHolding = true;
                        this.gameObject.GetComponent<PlayerController>().canJump = false;
                    }                
                }
                //Lever
                else if(grabcheck.collider != null && grabcheck.collider.tag == "Lever")
                {
                    grabcheck.collider.gameObject.GetComponent<Lever>().LeverOn();
                }
            }
            else if (isHolding)
            {
                RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist);
                if ((grabRay.collider != null && grabRay.collider.tag == "Box" || grabRay.collider != null && grabRay.collider.tag == "Player"))
                {
                    RaycastHit2D placeCheck = Physics2D.Raycast(placeChecker.position, Vector2.right * transform.localScale, raydist);
                    if(placeCheck.collider == null)
                    {
                        isHolding = false;
                    }             
                }
                else
                {
                    isHolding = false;
                }
            }
        }

        if (Input.GetKey(throwing) && isHolding)
        {
            StartCoroutine(PlayerThrowStop());
            isthrowing = true;
            throwforce += 0.1f;
            PowerCanvas.gameObject.SetActive(true);
            fill.color = gradient.Evaluate(1f);
            PowerCanvas.value = throwforce / 9f;
            fill.color = gradient.Evaluate(PowerCanvas.normalizedValue);
        }

        if (Input.GetKeyUp(throwing) && throwforce <= 2.6f)
        {
            throwforce = 2.7f;
        }

        if (throwforce >= 9f && isHolding)
        {
            anim.SetBool("thrown", true);
            throwforce = 9f;
        }
        if (Input.GetKeyUp(throwing) && isHolding)
        {
            StartCoroutine(PlayerThrowStop());
            anim.SetBool("thrown", true);
        }
        if (!isHolding)
        {
            throwforce = 0;
            PowerCanvas.gameObject.SetActive(false);
        }
    }

    //get rid of the crate thing 
    //make the crate up the right way 

    
    private void Drop()
    {
        Physics2D.IgnoreCollision(crate.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
        GameObject interactive = grabcheck.collider.gameObject;
        StartCoroutine(InteractStop());
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        interactive.transform.parent = null;
        interactive.GetComponent<Rigidbody2D>().isKinematic = false;
        interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        isHolding = false;
         if (interactive.gameObject.GetComponent<PlayerController>() != null)
         {

            interactive.transform.SetPositionAndRotation(dropPlayer.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            interactive.gameObject.GetComponent<PlayerController>().enabled = true;
            interactive.gameObject.GetComponent<Animator>().enabled = true;
            interactive.gameObject.GetComponent<Interactive>().enabled = true;
         }
    }

    private void PickUpBox()
    {
        GameObject box = grabcheck.collider.gameObject;
        Physics2D.IgnoreCollision(crate.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        box.transform.parent = holdLocation;
        box.transform.position = holdLocation.position;
        box.GetComponent<Rigidbody2D>().isKinematic = true;
        isHolding = true;
    }

    private void PickUpPlayer()
    { 
        this.gameObject.GetComponent<PlayerController>().canJump = false;
        var Rb = grabcheck.collider.gameObject.GetComponent<Rigidbody2D>();
        Rb.velocity = Vector3.zero;
        grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, -90)));
        grabcheck.collider.gameObject.transform.parent = holdLocation;
        grabcheck.collider.gameObject.transform.position = holdLocation.position;
        Rb.isKinematic = true;
        if (grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
        {
            //grabcheck.collider.gameObject.GetComponent<Animation>().Play("Hold");
            grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = false;
            grabcheck.collider.gameObject.GetComponent<PlayerController>().enabled = false;
            grabcheck.collider.gameObject.GetComponent<Animator>().enabled = false;
            isHolding = true;
        }
    }

    public void Throw()
    {
        StartCoroutine(PlayerThrowStop());
        isHolding = false;
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        grabcheck.collider.gameObject.transform.parent = null;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
        grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            if(grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
            {
                grabcheck.collider.gameObject.GetComponent<PlayerController>().controller();
                grabcheck.collider.gameObject.GetComponent<Animator>().enabled = true;
                grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = true;
            }
        Invoke("ResetThrow", 0.5f);
        
    }

    IEnumerator InteractStop()
    {
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
    }

    IEnumerator PlayerThrowStop()
    {
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
    }

    private void ResetThrow()
    {
        throwforce = 0;
        Physics2D.IgnoreCollision(crate.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
    }
}
