
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
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
    [SerializeField]
    private KeyCode throwing;

  


    public bool isHolding = false;
    public bool isthrowing = false;
    public bool Thrown = false;

    public static bool thrownMug = false;

    [SerializeField]
    private float throwforce;
    public float maxThrowForce;

    
    public int helddown = 0;


    private RaycastHit2D grabcheck;

    //throwing
    //[SerializeField]
    //private float timerThrow;
    //public bool canThrow = false;
    [SerializeField]
    private Slider PowerCanvas;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private float Offset;

    [SerializeField]
    private LayerMask CamLayer;

    private ChangeAnimationStateController stateC;


   //[HideInInspector]
    public bool pickedUpAxe = false;
    [HideInInspector]
    public bool pickedUpSpear = false;
    [HideInInspector]
    public bool pickedUpMead = false;


    public bool thrownLeft = false;
    public bool thrownRight = false;

    private PlayerController pc;


    void Start()
    {
        PowerCanvas.gameObject.SetActive(false);
        stateC = GetComponent<ChangeAnimationStateController>();
        pc = GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update()
    {     
        PowerCanvas.gameObject.transform.position = this.gameObject.transform.position + Vector3.up * Offset;
        if (Input.GetKey(interact))
        {
            helddown++;  
            //pressing key behavior
            if(helddown == 1)
            {
                helddown = 1;
                if (!isHolding)
                {
                    grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist, ~CamLayer);
                    //timerThrow = 0;
                    //box
                    if (grabcheck.collider != null)
                    {
                        PickingUpItem();
                    }
                }
               
                //first frame of pressing
            }
            else if(helddown > 68 && isHolding)  //was 68 //build needs to be 12
            {
                ChargingThrow();
            }
        }
        else if(!Input.GetKey(interact) && helddown > 0)
        {
            if(helddown == 1)
            {
                PickingUpItem();
            }
            //release behavior
            if(helddown < 68)  //12 for build
            {
                if (isHolding && !isthrowing)
                {
                    //update this for the other throwables  
                    RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist, ~CamLayer);
                    if ((grabRay.collider != null && grabRay.collider.tag == "Box" || grabRay.collider != null && grabRay.collider.tag == "Player") || grabRay.collider != null && grabRay.collider.gameObject.layer == 15)
                    {
                        RaycastHit2D placeCheck = Physics2D.Raycast(placeChecker.position, Vector2.right * transform.localScale, raydist, ~CamLayer);
                        if (placeCheck.collider == null)
                        {
                            PuttingDownItem();
                        }
                    }
                }
            }
            else
            {
                if (isHolding)
                {
                    isthrowing = true;
                }         
            }
            helddown = 0;
        }
        else
        {
            helddown = 0;
        }

            if (Input.GetKeyUp(interact) && throwforce <= 2.6f && isthrowing)
            {
                throwforce = 2.7f;
                Thrown = true;
            }
            if (throwforce >= maxThrowForce && isHolding && isthrowing)
            {
                Thrown = true;
                throwforce = maxThrowForce;
            }
            if (Input.GetKeyUp(interact) && isHolding && isthrowing)
            {
                throwforce += 0;
                StartCoroutine(PlayerThrowStop());
                Thrown = true;
            }
            if (!isHolding)
            {
                throwforce = 0;
                PowerCanvas.gameObject.SetActive(false);
            }
    }

    private void ChargingThrow()
    {
        StartCoroutine(PlayerThrowStop());
        isthrowing = true;
        throwforce += 0.2f;
        PowerCanvas.gameObject.SetActive(true);
        fill.color = gradient.Evaluate(1f);
        PowerCanvas.value = throwforce / maxThrowForce;
        fill.color = gradient.Evaluate(PowerCanvas.normalizedValue);
    }
    
    private void Drop()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        StartCoroutine(InteractStop());
        interactive.GetComponent<Rigidbody2D>().isKinematic = false;
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        if (interactive.gameObject.GetComponent<PlayerController>() != null)
        {
            interactive.transform.SetPositionAndRotation(dropPlayer.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //grabcheck.collider.gameObject.GetComponent<Animator>().SetBool("playerholding", false);
            interactive.GetComponent<ChangeAnimationStateController>().enabled = true;
            interactive.GetComponent<PlayerController>().enabled = true;
            interactive.GetComponent<Animator>().enabled = true;
            interactive.GetComponent<Interact>().enabled = true;
        }
        else if (pickedUpAxe)
        {
            if (pc.facingLeft)
            { 
                interactive.transform.position = dropLocation.position;
                interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, 0))); //66
                interactive.transform.localScale = Vector3.one;
            }
            else if (!pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, -0))); //-66
                interactive.transform.localScale = Vector3.one;
            }
            //pickedUpAxe = false;
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<Axe>().enabled = false;
            interactive.GetComponent<Axe>().TurnOn();
        }
        else if (pickedUpSpear)
        { 
            if (pc.facingLeft) //gameObject.transform.localScale.x < 0
            {
                interactive.transform.position = dropLocation.position;
                interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
                interactive.transform.localScale = Vector3.one;
            }            
            else if (!pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, -180)));
                interactive.transform.localScale = Vector3.one;
            }         
            //pickedUpSpear = false;
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<Spear>().enabled = false;
            interactive.GetComponent<Spear>().TurnOn();
        }
        else if (pickedUpMead)
        {
            if (pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
                interactive.transform.localScale = Vector3.one;
            }
            else if (!pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, -180)));
                interactive.transform.localScale = Vector3.one;
            }
            //pickedUpMead = false;
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<MeadPowerUp>().enabled = false;
            interactive.GetComponent<MeadPowerUp>().TurnOn();
        }
        else if(interactive.tag == "Box")
        {
            interactive.transform.position = dropLocation.position;
            interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Physics2D.IgnoreCollision(interactive.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
        }
        interactive.transform.parent = null;
        isHolding = false;
    }

    private void PickUp()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        isHolding = true;
        if (interactive.GetComponent<PlayerController>() != null)
        {
            interactive.GetComponent<PlayerController>().enabled = false;
            //Should take this out later
            interactive.GetComponent<ChangeAnimationStateController>().enabled = false;
            interactive.GetComponent<Animator>().enabled = false;
            this.gameObject.GetComponent<PlayerController>().canJump = false;
            //interactive.GetComponent<Animator>().SetBool("playerholding", true);
            interactive.GetComponent<Interact>().enabled = false;
            //Needs to be changed back to 0, 0, 0
            interactive.transform.SetPositionAndRotation(playerHold.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            interactive.transform.parent = playerHold;
            interactive.transform.position = playerHold.position;
            Rigidbody2D Rb = interactive.GetComponent<Rigidbody2D>();
            Rb.velocity = Vector3.zero;
            Rb.isKinematic = true;
        }
        else
        {
            Physics2D.IgnoreCollision(interactive.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
            interactive.transform.parent = holdLocation;
            interactive.transform.position = holdLocation.position;
            interactive.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
    private void Throw()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        interactive.GetComponent<Rigidbody2D>().isKinematic = false;
        StartCoroutine(PlayerThrowStop());
        Invoke("ResetThrow", 0.2f);
        if (grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
        {
            this.gameObject.GetComponent<PlayerController>().canJump = true;
            interactive.GetComponent<ChangeAnimationStateController>().enabled = true;
            interactive.GetComponent<PlayerController>().controller();
            interactive.GetComponent<Animator>().enabled = true;
            interactive.GetComponent<Interact>().enabled = true;
            interactive.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
            interactive.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else if (interactive.layer == 15)
        {
            interactive.GetComponent<Rigidbody2D>().isKinematic = false;
            interactive.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.1f) * throwforce;
            if (pickedUpAxe)
            {
                if (pc.facingLeft)
                {
                    interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, 66))); //66
                    interactive.transform.localScale = Vector3.one;
                }
                else if (!pc.facingLeft)
                {
                    interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, -66))); //-66
                    interactive.transform.localScale = Vector3.one;
                }
                //pickedUpAxe = false;
                Invoke("DropItems", 0.2f);
                interactive.GetComponent<Axe>().enabled = false;
                interactive.GetComponent<Axe>().TurnOn();
            }
            else if (pickedUpSpear)
            {
                if (pc.facingLeft)
                {
                    interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, 90)));
                    interactive.transform.localScale = Vector3.one;
                }
                else if (!pc.facingLeft)
                {
                   interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, -90)));
                   interactive.transform.localScale = Vector3.one;
                }       
            }
            else if (pickedUpMead)
            {
                if (pc.facingLeft)
                {
                    interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, 66)));
                    interactive.transform.localScale = Vector3.one;
                }
                else if (!pc.facingLeft)
                {
                    interactive.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(new Vector3(0, 0, -66)));
                    interactive.transform.localScale = Vector3.one;
                }
            }
            if (grabcheck.collider.gameObject.GetComponent<Axe>() != null)
            {
                interactive.GetComponent<Axe>().enabled = true;
                interactive.GetComponent<Axe>().TurnOn();
                interactive.GetComponent<Axe>().thrown = true;
                pickedUpAxe = false;
            }
            else if (grabcheck.collider.gameObject.GetComponent<MeadPowerUp>() != null)
            {
                thrownMug = true;
                interactive.GetComponent<MeadPowerUp>().enabled = true;
                interactive.GetComponent<MeadPowerUp>().TurnOn();
                interactive.GetComponent<MeadPowerUp>().thrown = true;
                pickedUpMead = false;
            }
            else if (grabcheck.collider.gameObject.GetComponent<Spear>() != null)
            {
                interactive.GetComponent<Spear>().enabled = true;
                interactive.GetComponent<Spear>().TurnOn();
                interactive.GetComponent<Spear>().thrown = true;
                pickedUpSpear = false;
            }
        }
        else
        {
            this.gameObject.GetComponent<PlayerController>().canJump = true;
            interactive.GetComponent<Rigidbody2D>().simulated = true;
            interactive.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
            interactive.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        interactive.transform.parent = null;
        isHolding = false;
        isthrowing = false;
        Thrown = false;
    }
    IEnumerator InteractStop()
    {
        //gameObject.GetComponent<PlayerController>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        yield return new WaitForSeconds(0.3f);
        //gameObject.GetComponent<PlayerController>().enabled = true;
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
    }
    IEnumerator PlayerThrowStop()
    {
        //gameObject.GetComponent<PlayerController>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        yield return new WaitForSeconds(0.7f);
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
        //gameObject.GetComponent<PlayerController>().enabled = true;
    }

    private void ResetThrow()
    {
        //if (thrownMug)
        // {
        //    throwforce = 0;
        // }
        //else
        // {
        Physics2D.IgnoreCollision(grabcheck.collider.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
        // }
    }

    public void PickingUpItem()
    {
        string tag = grabcheck.collider.tag;
        StartCoroutine(InteractStop());
        if (tag == "Box")
        {
            {
                //StartCoroutine(InteractStop());
                isHolding = true;
                this.gameObject.GetComponent<PlayerController>().canJump = false;
            }
        }
        //player
        else if (tag == "Player")
        {
            if (grabcheck.collider.gameObject.GetComponent<Interact>().isHolding == false)
            {
                isHolding = true;
                this.gameObject.GetComponent<PlayerController>().canJump = false;
            }
        }
        //Lever
        else if (tag == "Lever")
        {
            grabcheck.collider.gameObject.GetComponent<Lever>().LeverOn();
        }
        //Axe
        else if (tag == "Axe")
        {
            Axe axe = grabcheck.collider.gameObject.GetComponent<Axe>();
            pickedUpAxe = true;
            isHolding = true;
            axe.TurnOff();
            axe.enabled = false;

        }
        //Spear
        else if (tag == "Spear")
        {
            pickedUpSpear = true;
            isHolding = true;
            grabcheck.collider.gameObject.GetComponent<Spear>().TurnOff();
            grabcheck.collider.gameObject.GetComponent<Spear>().enabled = false;
        }
        //PowerThrowForce
        else if (tag == "Mug")
        {
            pickedUpMead = true;
            isHolding = true;
            grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().TurnOff();
            grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().enabled = false;
        }
    }

    public void PuttingDownItem()
    {
        isHolding = false;
        StartCoroutine(InteractStop());
    }


    public void StartThrowForce()
    {
        StartCoroutine(PowerUpThrow());
    }

    IEnumerator PowerUpThrow()
    {
        maxThrowForce = 15f;
        yield return new WaitForSeconds(10f);
        maxThrowForce = 11f;
    }

    private void CanThrowToggle()
    {
        //canThrow = true;
        //timerThrow = 0;
    }

    private void DropItems()
    {
        pickedUpAxe = false;
        pickedUpMead = false;
        pickedUpSpear = false;
    }

}
