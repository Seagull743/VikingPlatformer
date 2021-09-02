using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Holdables
{
    nothing, Crate, Player, Axe, Spear, Mug
}

public class ChangeAnimationStateController : MonoBehaviour
{
    public Holdables held = Holdables.nothing;

    [SerializeField]
    private Animator anim;
    private string currentState;
    //Animation states;
    public string PlayerIdle = "Player 1 Idle";
    public string PlayerRun = "Player1 Run";
    public string PlayerTakeOff = "Player 1 Take off";
    public string PlayerJump = "Player 1 Jump";
    public string PlayerLanding = "Player1Landing";
    //Interactive animations
    public string PlayerPickup = "Player1 Box Pickup";
    public string PlayerHoldingIdle = "Player1 holdingIdle";
    public string PlayerHoldingRun = "Player 1 holding Run";
    public string PlayerPutDown = "Player 1 Put Down";
    public string PlayerBoxPlayerChargeThrow = "Player 1 Throw";
    public string PlayerThrowActionBoxPlayer = "Player 1 Throw Action";

    //Axe Animations
    public string AxeIdle = "P1 Axe Idle";
    public string AxeJump = "P1 Axe Jump";
    public string AxeRun = "P1 Axe Run";
    public string AxeThrowAction = "P1 Axe Throw Action";
    public string AxeChargeThrow = "P1 Axe Throw";



    //isGrounded && canJump

    private PlayerController PC;
    private Interactive Interact;

    //Bools PlayerController
    [SerializeField] private bool isrunning;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool CanJump;
    [SerializeField] private bool isjumping;
    //Bools Interactive
    [SerializeField] private bool isholding;
    [SerializeField] private bool isThrowing;
    [SerializeField] private bool isthrown;
    [SerializeField] private bool pickuped;
    [SerializeField] private bool putDown;

    [SerializeField] private bool pickedupAxe;


    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<PlayerController>();
        Interact = GetComponent<Interactive>();
        putDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Player controller Bools
        isrunning = PC.isRunning;
        isjumping = PC.isJumping;
        isGrounded = PC.isGrounded;
        CanJump = PC.canJump;

        //Interactive Bools
        isholding = Interact.isHolding;
        isThrowing = Interact.isthrowing;
        isthrown = Interact.Thrown;

        pickedupAxe = Interact.pickedUpAxe;

        //Running Animations
        if (!isholding)
        {
            if (!putDown)
            {
                PutDown();
            }
            else if (putDown)
            {
                if (isGrounded)
                {
                    if (isrunning)
                    {
                        Run();
                    }
                    else if (!isrunning)
                    {
                        Idle();
                    }
                }
                else if (!isGrounded)
                {
                    Jump();
                }
            }        
        }
        else if (isholding)
        {
            if (!pickuped)
            {
                Pickup();
            }
            else if(pickuped)
            {
                if (isGrounded)
                {
                    if (isThrowing)
                    {
                        if (!isthrown)
                        {
                            ChargeThrow();
                        }
                        else if (isthrown)
                        {
                            Thrown();
                        }
                    }
                    else if (!isThrowing)
                    {
                        if (isrunning)
                        {
                            HoldingRun();
                        }
                        else if (!isrunning)
                        {
                            HoldingIdle();
                        }
                    }
                }
                else if (!isGrounded)
                {
                    Jump();
                }
            }       
        }

        //Throwing Script
       //if (isholding)
        //{
        //    //if (isThrowing)
        //    {
        //        if (!isthrown)
        //        {
        //            ChargeThrow();
        //        }
        //        else if (isthrown)
        //        {
        //            Thrown();
        //        }
        //    }
        //}
    }
    

    public void Jump()
    {
        if (pickedupAxe)
        {
            ChangeAnimationState(AxeJump);
        }
        else
        {
            ChangeAnimationState(PlayerJump);
        }  
    }

    public void Idle()
    {
        ChangeAnimationState(PlayerIdle);   
    }

    public void HoldingIdle()
    {
        if (pickedupAxe)
        {
            ChangeAnimationState(AxeIdle);
        }
        else
        {
            ChangeAnimationState(PlayerHoldingIdle);
        }     
    }

   //Could get rid of this method if you do a if !holding same with idle
    public void Run()
    {
         ChangeAnimationState(PlayerRun);
    }

    public void HoldingRun()
    {
        if (pickedupAxe)
        {
            ChangeAnimationState(AxeRun);
        }
        else
        {
            ChangeAnimationState(PlayerHoldingRun);
        }    
    }
    public void ChargeThrow()
    {
        if (pickedupAxe)
        {
            ChangeAnimationState(AxeChargeThrow);
        }
        else
        {
            ChangeAnimationState(PlayerBoxPlayerChargeThrow);
        } 
        //if holding a box play that throw charge etc
    }
    
    public void Thrown()
    {
        if (pickedupAxe)
        {
            ChangeAnimationState(AxeThrowAction);
        }
        else
        {
            ChangeAnimationState(PlayerThrowActionBoxPlayer);
        }   
        //if I'm holding a bxo play that throw anim etc
    }
    
    public void Pickup()
    {
        //if(held == Holdables.Axe)
        //{
        //}

        ChangeAnimationState(PlayerPickup);
        // If I'm holding a box play box pickup animation
        // If holding an axe play axe pickup animation
    }

    public void PutDown()
    {
        ChangeAnimationState(PlayerPutDown);
        // If I'm holding a box play box putdown animation
        // If holding an axe play axe putdown animation
    }

    public void FinshedPickUp()
    {
        pickuped = true;
        putDown = false;     
    }

    public void FinishedDrop()
    {
        putDown = true;
        pickuped = false;
    }

    public void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play animation state
        anim.Play(newState);

        //reassign the current state
        currentState = newState;
    }

}
