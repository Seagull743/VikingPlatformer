﻿
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

  


    [HideInInspector] public bool isHolding = false;
    [HideInInspector] public bool isthrowing = false;
    [HideInInspector] public bool Thrown = false;

    public static bool thrownMug = false;

    [SerializeField]
    private float throwforce;
    public float maxThrowForce;

    private RaycastHit2D grabcheck;

    //throwing
    [SerializeField]
    private float timerThrow;
    public bool canThrow = false;
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


    [HideInInspector]
    public bool pickedUpAxe = false;
    [HideInInspector]
    public bool pickedUpSpear = false;
    [HideInInspector]
    public bool pickedUpMead = false;

    void Start()
    {
        PowerCanvas.gameObject.SetActive(false);
        stateC = GetComponent<ChangeAnimationStateController>();
    }
    // Update is called once per frame
    void Update()
    {
        PowerCanvas.gameObject.transform.position = this.gameObject.transform.position + Vector3.up * Offset;

        if (Input.GetKeyDown(interact) && throwforce == 0)
        {
            if (!isHolding)
            {
                grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist, ~CamLayer);
                //box

                if (grabcheck.collider != null)
                {
                    StartCoroutine(InteractStop());
                    if (grabcheck.collider.tag == "Box")
                    {
                        {
                            //StartCoroutine(InteractStop());
                            isHolding = true;
                            this.gameObject.GetComponent<PlayerController>().canJump = false;
                        }
                    }
                    //player
                    else if (grabcheck.collider.tag == "Player")
                    {
                        if (grabcheck.collider.gameObject.GetComponent<Interact>().isHolding == false)
                        {
                            isHolding = true;
                            this.gameObject.GetComponent<PlayerController>().canJump = false;
                        }
                    }
                    //Lever
                    else if (grabcheck.collider.tag == "Lever")
                    {
                        grabcheck.collider.gameObject.GetComponent<Lever>().LeverOn();
                    }
                    //Axe
                    else if (grabcheck.collider.tag == "Axe")
                    {
                        pickedUpAxe = true;
                        isHolding = true;
                        grabcheck.collider.gameObject.GetComponent<Axe>().TurnOff();
                        grabcheck.collider.gameObject.GetComponent<Axe>().enabled = false;

                    }
                    //Spear
                    else if (grabcheck.collider.tag == "Spear")
                    {
                        pickedUpSpear = true;
                        isHolding = true;
                        grabcheck.collider.gameObject.GetComponent<Spear>().TurnOff();
                        grabcheck.collider.gameObject.GetComponent<Spear>().enabled = false;
                    }
                    //PowerThrowForce
                    else if (grabcheck.collider.tag == "Mug")
                    {
                        pickedUpMead = true;
                        isHolding = true;
                        grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().TurnOff();
                        grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().enabled = false;
                    }
                }
            }
            else if (isHolding)
            {
                //update this for the other throwables  
                RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist, ~CamLayer);
                if ((grabRay.collider != null && grabRay.collider.tag == "Box" || grabRay.collider != null && grabRay.collider.tag == "Player") || grabRay.collider != null && grabRay.collider.gameObject.layer == 15)
                {
                    RaycastHit2D placeCheck = Physics2D.Raycast(placeChecker.position, Vector2.right * transform.localScale, raydist, ~CamLayer);
                    if (placeCheck.collider == null)
                    {
                        isHolding = false;
                        StartCoroutine(InteractStop());
                    }
                }
            }
        }
        if (Input.GetKey(interact) && isHolding && canThrow)
        {
            timerThrow += 0.1f;
            if(timerThrow >= 3f)
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


        if (Input.GetKeyUp(interact) && throwforce <= 2.6f && isthrowing)
        {
            throwforce = 2.7f;
            Thrown = true;
        }
        if (throwforce >= maxThrowForce && isHolding && isthrowing)
        {
            //anim.SetBool("thrown", true);
            Thrown = true;
            throwforce = maxThrowForce;
        }
        if (Input.GetKeyUp(interact) && isHolding && isthrowing)
        {
            throwforce += 0;
            StartCoroutine(PlayerThrowStop());
            //anim.SetBool("thrown", true);
            Thrown = true;
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
        canThrow = false;
        GameObject interactive = grabcheck.collider.gameObject;
        Physics2D.IgnoreCollision(interactive.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), false);
        StartCoroutine(InteractStop());
        this.gameObject.GetComponent<PlayerController>().canJump = true;
        interactive.transform.parent = null;
        interactive.GetComponent<Rigidbody2D>().isKinematic = false;
        interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        if (interactive.gameObject.GetComponent<PlayerController>() != null)
        {
            interactive.transform.SetPositionAndRotation(dropPlayer.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //grabcheck.collider.gameObject.GetComponent<Animator>().SetBool("playerholding", false);
            interactive.GetComponent<ChangeAnimationStateController>().enabled = true;
            interactive.gameObject.GetComponent<PlayerController>().enabled = true;
            interactive.gameObject.GetComponent<Animator>().enabled = true;
            interactive.gameObject.GetComponent<Interact>().enabled = true;
        }
        if (pickedUpAxe)
        {
            if (gameObject.transform.localScale.x < 0)
                interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 66)));
            else if (gameObject.transform.localScale.x > 0)
                interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, -66)));
            //pickedUpAxe = false;
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<Axe>().enabled = false;
            interactive.GetComponent<Axe>().TurnOn();
        }
        else if (pickedUpSpear)
        {
            if (gameObject.transform.localScale.x < 0)
                interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else if (gameObject.transform.localScale.x > 0)
                interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, -0)));
            //pickedUpSpear = false;
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<Spear>().enabled = false;
            interactive.GetComponent<Spear>().TurnOn();
        }
        else if (pickedUpMead)
        {
            if (gameObject.transform.localScale.x < 0)
                interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else if (gameObject.transform.localScale.x > 0)
                interactive.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, -0)));
            //pickedUpMead = false;
            Invoke("DropItems", 0.2f);
            interactive.GetComponent<MeadPowerUp>().enabled = false;
            interactive.GetComponent<MeadPowerUp>().TurnOn();
        }
        isHolding = false;
    }

    private void PickUp()
    {
        GameObject interactive = grabcheck.collider.gameObject;
        timerThrow = 0;
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
        //interactive.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //Physics2D.IgnoreCollision(interactive.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        //interactive.transform.parent = holdLocation;
        //interactive.transform.position = holdLocation.position;
        //interactive.GetComponent<Rigidbody2D>().isKinematic = true;
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
        timerThrow = 0;
        isHolding = false;
        isthrowing = false;
        Thrown = false;
        canThrow = false;
        Invoke("ResetThrow", 0.2f);
        if (grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
        {
            // StartCoroutine(PlayerThrowStop());
            grabcheck.collider.gameObject.GetComponent<ChangeAnimationStateController>().enabled = true;
            grabcheck.collider.gameObject.GetComponent<PlayerController>().controller();
            grabcheck.collider.gameObject.GetComponent<Animator>().enabled = true;
            grabcheck.collider.gameObject.GetComponent<Interact>().enabled = true;
        }
        if (grabcheck.collider.gameObject.layer == 15)
        {
            this.gameObject.GetComponent<PlayerController>().canJump = true;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            grabcheck.collider.gameObject.transform.parent = null;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.1f) * throwforce;

            if (pickedUpAxe)
            {
                if (gameObject.transform.localScale.x < 0)
                {
                    grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 40)));
                }
                else if (gameObject.transform.localScale.x > 0)
                    grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, -40)));
            }
            if (pickedUpSpear)
            {
                if (gameObject.transform.localScale.x < 0)
                {
                    grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (gameObject.transform.localScale.x > 0)
                {
                    grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, -0)));
                }

                if (pickedUpMead)
                {
                    if (gameObject.transform.localScale.x < 0)
                    {
                        grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                    else if (gameObject.transform.localScale.x > 0)
                    {
                        grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, -0)));
                    }
                }
            }
            if (grabcheck.collider.gameObject.GetComponent<Axe>() != null)
            {
                grabcheck.collider.gameObject.GetComponent<Axe>().enabled = true;
                grabcheck.collider.gameObject.GetComponent<Axe>().TurnOn();
                grabcheck.collider.gameObject.GetComponent<Axe>().thrown = true;
                pickedUpAxe = false;
            }
            else if (grabcheck.collider.gameObject.GetComponent<MeadPowerUp>() != null)
            {
                thrownMug = true;
                grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().enabled = true;
                grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().TurnOn();
                grabcheck.collider.gameObject.GetComponent<MeadPowerUp>().thrown = true;
                pickedUpMead = false;
            }
            else if (grabcheck.collider.gameObject.GetComponent<Spear>() != null)
            {
                grabcheck.collider.gameObject.GetComponent<Spear>().enabled = true;
                grabcheck.collider.gameObject.GetComponent<Spear>().TurnOn();
                grabcheck.collider.gameObject.GetComponent<Spear>().thrown = true;
                pickedUpSpear = false;
            }
        }
        else
        {
            StartCoroutine(PlayerThrowStop());
            this.gameObject.GetComponent<PlayerController>().canJump = true;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            grabcheck.collider.gameObject.transform.parent = null;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.5f) * throwforce;
            grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
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
        yield return new WaitForSeconds(0.5f);
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
        canThrow = true;
        timerThrow = 0;
    }

    private void DropItems()
    {
        pickedUpAxe = false;
        pickedUpMead = false;
        pickedUpSpear = false;
    }

}