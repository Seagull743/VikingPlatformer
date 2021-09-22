
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
    private Transform playerHold;
    [SerializeField]
    private Transform dropLocation;
    [SerializeField]
    private Transform dropPlayer;
    [SerializeField]
    private float raydist;
    [SerializeField]
    private KeyCode interact;
    //[SerializeField]
   // private KeyCode throwing;
    public bool isHolding = false;

    private bool isthrowing = false;
    private bool canThrow = false;

    public float timerThrow;


    [SerializeField]
    private float throwforce;
    [SerializeField]
    private float maxThrowForce;

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
    private LayerMask CamLayer;


    void Start()
    {
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

        if (Input.GetKeyDown(interact) && throwforce == 0)
        {
            if (!isHolding)
            {
               
                grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist, ~CamLayer);
                //box
                if (grabcheck.collider != null && grabcheck.collider.tag == "Box")
                {
                    {                    
                        StartCoroutine(InteractStop());
                        Invoke("CanThrowToggle", 0.2f);
                        isHolding = true;
                        this.gameObject.GetComponent<PlayerController>().canJump = false;              
                    }                 
                }
                //player
                else if (grabcheck.collider != null && grabcheck.collider.tag == "Player")
                {
                    if(grabcheck.collider.gameObject.GetComponent<Interactive>() != null)
                    {
                        if(grabcheck.collider.gameObject.GetComponent<Interactive>().isHolding == false)
                        {
                            StartCoroutine(InteractStop());
                            Invoke("CanThrowToggle", 0.2f);
                            isHolding = true;
                            this.gameObject.GetComponent<PlayerController>().canJump = false;
                        }
                        else
                        {
                            isHolding = false;
                        }
                    }                
                }
                //Lever
                else if(grabcheck.collider != null && grabcheck.collider.tag == "Lever")
                {
                    grabcheck.collider.gameObject.GetComponent<Lever>().LeverOn();
                }
            }
            //else if (isHolding && !isthrowing)
           // {
              //  RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist, ~CamLayer);
               // if ((grabRay.collider != null && grabRay.collider.tag == "Box" || grabRay.collider != null && grabRay.collider.tag == "Player"))
               // {
                //    RaycastHit2D placeCheck = Physics2D.Raycast(placeChecker.position, Vector2.right * transform.localScale, raydist, ~CamLayer);
                 //   if(placeCheck.collider == null)
                  //  {
                  //      isHolding = false;
                   //     canThrow = false;
                  //  }             
                //}
               // else
               // {
                //    isHolding = false;
                //    canThrow = false;
             //   }
           // }
        }

        if (Input.GetKey(interact) && isHolding && canThrow)
        {           
            timerThrow += 0.1f;
            if(timerThrow > 3f)
            {
                StartCoroutine(PlayerThrowStop());
                isthrowing = true;
                throwforce += 0.2f;
                PowerCanvas.gameObject.SetActive(true);
                fill.color = gradient.Evaluate(1f);
                PowerCanvas.value = throwforce / maxThrowForce;
                fill.color = gradient.Evaluate(PowerCanvas.normalizedValue);
            }       
        }

        if (Input.GetKeyUp(interact) && !isthrowing)
        {
            isHolding = false;
        }
        
        if(Input.GetKeyUp(interact) && isthrowing && throwforce <= 2.9f)
        {
            throwforce = 3f;
        }

   
        if (throwforce >= maxThrowForce && isHolding && isthrowing)
        {
            anim.SetBool("thrown", true);
            StartCoroutine(PlayerThrowStop());
            throwforce = maxThrowForce;
        }
        if (Input.GetKeyUp(interact) && isHolding && isthrowing)
        {
            throwforce += 0;
            StartCoroutine(PlayerThrowStop());
            anim.SetBool("thrown", true);
        }
        if (!isHolding)
        {
            throwforce = 0;
            PowerCanvas.gameObject.SetActive(false);
        }
    }

    private void Drop()
    {
        timerThrow = 0;
        Physics2D.IgnoreCollision(grabcheck.collider.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
        StartCoroutine(InteractStop());
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        grabcheck.collider.gameObject.transform.parent = null;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        grabcheck.collider.gameObject.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
         if (grabcheck.collider.gameObject.gameObject.GetComponent<PlayerController>() != null)
         {

            grabcheck.collider.gameObject.transform.SetPositionAndRotation(dropPlayer.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            grabcheck.collider.gameObject.GetComponent<Animator>().SetBool("playerholding", false);
            grabcheck.collider.gameObject.gameObject.GetComponent<PlayerController>().enabled = true;
            grabcheck.collider.gameObject.gameObject.GetComponent<Animator>().enabled = true;
            grabcheck.collider.gameObject.gameObject.GetComponent<Interactive>().enabled = true;
         }
    }

    private void PickUp()
    {
        
        if(grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
        {
            isHolding = true;
            grabcheck.collider.gameObject.GetComponent<PlayerController>().enabled = false;
            this.gameObject.GetComponent<PlayerController>().canJump = false;
            grabcheck.collider.gameObject.GetComponent<Animator>().SetBool("playerholding", true);
            grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = false;
            grabcheck.collider.gameObject.transform.SetPositionAndRotation(playerHold.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            grabcheck.collider.gameObject.transform.parent = playerHold;
            grabcheck.collider.gameObject.transform.position = playerHold.position;
            Rigidbody2D Rb = grabcheck.collider.gameObject.GetComponent<Rigidbody2D>();
            Rb.velocity = Vector3.zero;
            Rb.isKinematic = true;
        }
        else
        {
            isHolding = true;
            Physics2D.IgnoreCollision(grabcheck.collider.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
            grabcheck.collider.gameObject.transform.parent = holdLocation;
            grabcheck.collider.gameObject.transform.position = holdLocation.position;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;         
        }     
    }

    private void Throw()
    {
        timerThrow = 0;
        StartCoroutine(PlayerThrowStop());
        isHolding = false;
        canThrow = false;
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        grabcheck.collider.gameObject.transform.parent = null;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
        grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            
        if(grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
            {
                grabcheck.collider.gameObject.GetComponent<Animator>().SetBool("playerholding", false);
                grabcheck.collider.gameObject.GetComponent<PlayerController>().controller();
                grabcheck.collider.gameObject.GetComponent<Animator>().enabled = true;
                grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = true;
            }
        Invoke("ResetThrow", 0.3f);
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

    private void CanThrowToggle()
    {
        canThrow = true;
        timerThrow = 0;
    }

    private void ResetThrow()
    {
        throwforce = 0;
        Physics2D.IgnoreCollision(grabcheck.collider.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
    }


}
