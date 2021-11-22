
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
    private LayerMask InteractLayer;

    private ChangeAnimationStateController stateC;


   //[HideInInspector]
    public bool pickedUpAxe = false;
    //[HideInInspector]
    public bool pickedUpSpear = false;
    //[HideInInspector]
    public bool pickedUpMead = false;
    //[HideInspector]
    public bool pickedUpHammer = false;
    public bool pickedUpCrate = false;
    public bool pickedUpPlayer = false;
    public bool thrownLeft = false;
    public bool thrownRight = false;

    private PlayerController pc;
    [SerializeField]
    private float interacttimer = 0.25f;
    private float currenttimer;
    public bool  Interactpressed = false;
    public bool pickingup = false;

    void Start()
    {
        currenttimer = interacttimer;
        PowerCanvas.gameObject.SetActive(false);
        stateC = GetComponent<ChangeAnimationStateController>();
        pc = GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if(isHolding && pickedUpCrate || isHolding && pickedUpPlayer)
        {
            pc.moveSpeed = 4;
        }
        else
        {
            pc.moveSpeed = 5;
        }


        if (interacttimer > 0 && Interactpressed)
        {
            interacttimer -= Time.deltaTime;
        }

        if(interacttimer <= 0 && Interactpressed)
        {
           Interactpressed = false;
           interacttimer = currenttimer;
         }

        PowerCanvas.gameObject.transform.position = this.gameObject.transform.position + Vector3.up * Offset;
        if (Input.GetKey(interact) && !Interactpressed)
        {
            helddown++;  
            //pressing key behavior
            if(helddown == 1)
            {
                helddown = 1;
                if (!isHolding)
                {
                    grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist, ~InteractLayer);
                    //timerThrow = 0;
                    //box
                    if (grabcheck.collider != null)
                    {
                        PickingUpItem();
                    }
                }
               
                //first frame of pressing
            }
            else if(helddown > 12 && isHolding)  //was 68 //build needs to be 9
            {
                ChargingThrow();
            }
        }
        else if(!Input.GetKey(interact) && helddown > 0)
        {
            Interactpressed = true;  
            if(helddown == 1)
            {
                PickingUpItem();
            }
            //release behavior
            if(helddown < 12)  //9 for build  //68 for unity
            {
                if (isHolding && !isthrowing)
                {
                    RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist, ~InteractLayer);
                    if(grabRay.collider != null)
                    {
                        if (grabRay.collider.tag == "Box" || grabRay.collider.tag == "Player" || grabRay.collider.gameObject.layer == 15 || grabRay.collider.gameObject.layer == 21) // layer 15 throwable
                        {
                            RaycastHit2D placeCheck = Physics2D.Raycast(placeChecker.position, Vector2.right * transform.localScale, raydist, ~InteractLayer);
                            if (placeCheck.collider == null)
                            {
                                PuttingDownItem();
                            }
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
            //less throw was 2.6f to 2.7f
            if (Input.GetKeyUp(interact) && throwforce <= 4.7f && isthrowing)
            {
                throwforce = 4.8f;
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
    
    public void Drop()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        StartCoroutine(InteractStop());
        Rigidbody2D irb = interactive.GetComponent<Rigidbody2D>();
        irb.isKinematic = false;
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        if (interactive.gameObject.GetComponent<PlayerController>() != null)
        {
            irb.transform.SetPositionAndRotation(dropPlayer.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //grabcheck.collider.gameObject.GetComponent<Animator>().SetBool("playerholding", false);
            interactive.GetComponent<ChangeAnimationStateController>().enabled = true;
            interactive.GetComponent<PlayerController>().enabled = true;
            interactive.GetComponent<Animator>().enabled = true;
            interactive.GetComponent<Interact>().enabled = true;
            pickedUpPlayer = false;
        }
        else if (pickedUpAxe)
        {
            Axe axe = interactive.GetComponent<Axe>();
            irb.freezeRotation = false;
            if (pc.facingLeft)
            { 
                axe.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, 90)));
                //irb.transform.localScale = new Vector3(1, 1, 1); // 1, -1, 1
                axe.transform.localScale = Vector3.one;
            }
            else if (!pc.facingLeft)
            {
                axe.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, -90)));
                axe.transform.localScale = Vector3.one;
            }
            Invoke("DropItems", 0.2f);
            axe.enabled = false;
            axe.TurnOn();
        }
        else if (pickedUpSpear)
        {
            irb.freezeRotation = false;
            if (pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
                interactive.transform.localScale = Vector3.one;
            }            
            else if (!pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, -180)));
                interactive.transform.localScale = Vector3.one;
            }        
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<Spear>().enabled = false;
            interactive.GetComponent<Spear>().TurnOn();
        }
        else if (pickedUpMead)
        {
            if (pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
                interactive.transform.localScale = Vector3.one;
            }
            else if (!pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, -180)));
                interactive.transform.localScale = Vector3.one;
            }
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<MeadPowerUp>().enabled = false;
            interactive.GetComponent<MeadPowerUp>().TurnOn();
        }
        else if(pickedUpHammer)
        {
            if (pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
                interactive.transform.localScale = Vector3.one;
            }
            else if (!pc.facingLeft)
            {
                interactive.transform.position = dropLocation.position;
                irb.SetRotation(Quaternion.Euler(new Vector3(0, 0, -180)));
                interactive.transform.localScale = Vector3.one;
            }
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<Hammer>().enabled = false;
            interactive.GetComponent<Hammer>().TurnOn();
        }
        else if(interactive.tag == "Box")
        {
            pickedUpCrate = false;
            interactive.transform.position = dropLocation.position;
            interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //Physics2D.IgnoreCollision(interactive.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false); //check this
        }
        interactive.transform.parent = null;
        isHolding = false;
    }

    private void PickUp()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        Rigidbody2D irb = interactive.GetComponent<Rigidbody2D>();
        isHolding = true;
        if (interactive.GetComponent<PlayerController>() != null)
        {    
            interactive.GetComponent<PlayerController>().enabled = false;
            interactive.GetComponent<ChangeAnimationStateController>().enabled = false;
            interactive.GetComponent<Animator>().enabled = false;
            this.gameObject.GetComponent<PlayerController>().canJump = false;
            interactive.GetComponent<Interact>().enabled = false;
            interactive.transform.SetPositionAndRotation(playerHold.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            interactive.transform.parent = playerHold;
            interactive.transform.position = playerHold.position;
            irb.velocity = Vector3.zero;
            irb.isKinematic = true;
        }
        else
        {
            //Physics2D.IgnoreCollision(interactive.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
            interactive.transform.parent = holdLocation;
            interactive.transform.position = holdLocation.position;
            irb.isKinematic = true;
        }
        Invoke("CheckHolding", 0.15f);
        Invoke("PickingUpFalseToggle", 0.4f);
    }
    private void Throw()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        Rigidbody2D irb = interactive.GetComponent<Rigidbody2D>();
        irb.simulated = false;
        irb.isKinematic = false;
        StartCoroutine(PlayerThrowStop());
        if (interactive.GetComponent<PlayerController>() != null)
        {
            irb.transform.SetParent(null);
            this.gameObject.GetComponent<PlayerController>().canJump = true;
            interactive.GetComponent<ChangeAnimationStateController>().enabled = true;
            interactive.GetComponent<PlayerController>().controller();
            interactive.GetComponent<Animator>().enabled = true;
            interactive.GetComponent<Interact>().enabled = true;
            irb.velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
            interactive.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            pickedUpPlayer = false;
            irb.simulated = true;
        }
        else if (interactive.layer == 15 || interactive.layer == 21)
        {
            irb.transform.SetParent(null);
            
            if (pickedUpAxe)
            {
                if (pc.facingLeft)
                {
                    irb.transform.localScale = new Vector3(-1, 1, 1);
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 60));
                }
                else if (!pc.facingLeft)
                {
                    interactive.transform.localScale = Vector3.one;
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
                }
                interactive.GetComponent<Axe>().enabled = true;
                interactive.GetComponent<Axe>().TurnOn();
                interactive.GetComponent<Axe>().thrown = true;
                interactive.GetComponent<Axe>().canKill = true;
                pickedUpAxe = false;
                irb.simulated = true;
                irb.isKinematic = false;
            }
            else if (pickedUpSpear)
            {
                if (pc.facingLeft)
                {
                    irb.transform.localScale = new Vector3(-1, 1, 1);
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));                
                }
                else if (!pc.facingLeft)
                {
                    interactive.transform.localScale = Vector3.one;
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    interactive.transform.localScale = Vector3.one;
                }

                interactive.GetComponent<Spear>().enabled = true;
                interactive.GetComponent<Spear>().TurnOn();
                interactive.GetComponent<Spear>().thrown = true;
                pickedUpSpear = false;
                irb.simulated = true;
                irb.isKinematic = false;
            }
            else if (pickedUpMead)
            {
                if (pc.facingLeft)
                {
                    irb.transform.localScale = new Vector3(-1, 1, 1);
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 20));
                }
                else if (!pc.facingLeft)
                {
                    interactive.transform.localScale = Vector3.one;
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -20));
                }
                thrownMug = true;
                interactive.GetComponent<MeadPowerUp>().enabled = true;
                interactive.GetComponent<MeadPowerUp>().TurnOn();
                interactive.GetComponent<MeadPowerUp>().thrown = true;
                pickedUpMead = false;
            }
            else if (pickedUpHammer)
            {
                if (pc.facingLeft)
                {
                    irb.transform.localScale = new Vector3(-1, 1, 1);
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 159));
                }
                else if (!pc.facingLeft)
                {
                    interactive.transform.localScale = Vector3.one;
                    irb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 159));
                }
                interactive.GetComponent<Hammer>().enabled = true;
                interactive.GetComponent<Hammer>().TurnOn();
                interactive.GetComponent<Hammer>().thrown = true;
                interactive.GetComponent<Hammer>().canKill = true;
                pickedUpHammer = false;
                irb.simulated = true;
                irb.isKinematic = false;
            }
            irb.simulated = true;
            irb.isKinematic = false;
            irb.velocity = new Vector2(transform.localScale.x, 0.1f) * throwforce;
        }
        else
        {
            irb.transform.SetParent(null);
            this.gameObject.GetComponent<PlayerController>().canJump = true;
            irb.isKinematic = false;
            irb.simulated = true;
            irb.velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
            interactive.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            pickedUpCrate = false;
            Invoke("ResetThrow", 0.2f);
        }
        //interactive.transform.parent = null;
        isHolding = false;
        isthrowing = false;
        Thrown = false;
    }
    IEnumerator InteractStop()
    {
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        yield return new WaitForSeconds(0.3f);
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
    }
    IEnumerator PlayerThrowStop()
    {
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        yield return new WaitForSeconds(0.7f);
        this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
    }

    private void ResetThrow()
    {
        //Physics2D.IgnoreCollision(grabcheck.collider.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
    }

    private void CheckHolding()
    {
        
        RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist, ~InteractLayer);
        if (grabRay.collider == null)
        {
            grabcheck = new RaycastHit2D();
            isHolding = false;
            pickedUpAxe = false;
            pickedUpSpear = false;
            pickedUpMead = false;
            pickedUpHammer = false;
            pickedUpCrate = false;
            pickedUpPlayer = false;
            pc.canJump = true;
            //Physics2D.IgnoreCollision(grabcheck.collider.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
            ResetThrow();
            gameObject.GetComponent<PlayerController>().moveSpeed = 5;
        }
        else if (grabRay.collider != null)
        {
            
            if (grabRay.collider.gameObject.GetComponent<Interact>() != null)
            {
                if (grabRay.collider.gameObject.GetComponent<Interact>().pickedUpCrate)
                {
                    isHolding = false;
                }
            }
            else
            {
                isHolding = true;
            }          
        }
    }
    public void PickingUpItem()
    {
        pickingup = true;
        string tag = grabcheck.collider.tag;
        StartCoroutine(InteractStop());
        if (tag == "Box")
        {
            {
                isHolding = true;
                pickedUpCrate = true;
                this.gameObject.GetComponent<PlayerController>().canJump = false;
            }
        }
        //player
        else if (tag == "Player")
        {
            if (grabcheck.collider.gameObject.GetComponent<Interact>().pickedUpCrate == false && grabcheck.collider.gameObject.GetComponent<Interact>().pickingup == false)
            {
                isHolding = true;
                pickedUpPlayer = true;
                this.gameObject.GetComponent<PlayerController>().canJump = false;
            }
        }
        //Lever
        //else if (tag == "Lever")
        //{
          //  grabcheck.collider.gameObject.GetComponent<Lever>().LeverOn();
        //}
        //Axe
        else if (tag == "Axe" && grabcheck.collider.gameObject.GetComponent<Axe>() != null)
        {
            Axe axe = grabcheck.collider.gameObject.GetComponent<Axe>();
            pickedUpAxe = true;
            isHolding = true;
            axe.TurnOff();
            axe.enabled = false;
            axe.thrown = false;

        }
        //Spear
        else if (tag == "Spear" && grabcheck.collider.gameObject.GetComponent<Spear>() != null)
        {
            Spear spear = grabcheck.collider.gameObject.GetComponent<Spear>();
            pickedUpSpear = true;
            spear.thrown = false;
            isHolding = true;
            spear.GetComponent<Spear>().TurnOff();
            spear.GetComponent<Spear>().enabled = false;
        }
        //PowerThrowForce
        else if (tag == "Mug")
        {
            MeadPowerUp mug = grabcheck.collider.gameObject.GetComponent<MeadPowerUp>();
            pickedUpMead = true;
            isHolding = true;
            mug.TurnOff();
            mug.enabled = false;
        }
        else if(tag == "Hammer" && grabcheck.collider.gameObject.GetComponent<Hammer>() != null)
        {
            Hammer hammer = grabcheck.collider.gameObject.GetComponent<Hammer>();
            pickedUpHammer = true;
            isHolding = true;
            hammer.TurnOff();
            hammer.enabled = false;
            hammer.thrown = false;
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

    public void PickingUpToggle()
    {
        pickingup = true;
    }

    public void PickingUpFalseToggle()
    {
        pickingup = false;
    }

    private void DropItems()
    {
        pickedUpAxe = false;
        pickedUpMead = false;
        pickedUpSpear = false;
        pickedUpHammer = false;
    }

}
