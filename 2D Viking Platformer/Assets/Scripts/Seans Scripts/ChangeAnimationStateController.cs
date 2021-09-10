using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players
{
   P1, P2
}

public class ChangeAnimationStateController : MonoBehaviour
{
    public Players player;

    [SerializeField]
    private Animator anim;
    private string currentState;
    //Animation states;
    private string PlayerIdle = "Player 1 Idle";
    private string PlayerRun = "Player1 Run";
    private string PlayerTakeOff = "Player 1 Take off";
    private string PlayerJump = "Player 1 Jump";
    private string PlayerLanding = "Player1Landing";
    //Interactive animations
    private string PlayerPickup = "Player1 Box Pickup";
    private string PlayerHoldingIdle = "Player1 holdingIdle";
    private string PlayerHoldingRun = "Player 1 holding Run";
    private string PlayerPutDown = "Player 1 Put Down";
    private string PlayerBoxPlayerChargeThrow = "Player 1 Throw";
    private string PlayerThrowActionBoxPlayer = "Player 1 Throw Action";

    //Axe Animations
    private string AxeIdle = "P1 Axe Idle";
    private string AxeJump = "P1 Axe Jump";
    private string AxeRun = "P1 Axe Run";
    private string AxeThrowAction = "P1 Axe Throw Action";
    private string AxeThrow = "P1 Axe Throw";

    //Spear Animations
    private string SpearIdle = "P1 Spear idle";
    private string SpearJump = "P1 Spear Jump";
    private string SpearRun = "P1 Spear Run";
    private string SpearThrow = "P1 Spear Throw";
    private string SpearThrowAction = "P1 Spear Throw Action";

    //Mead Animations
    private string MeadIdle = "P1 Mead Idle";
    private string MeadJump = "P1 Mead Jump";
    private string MeadRun = "P1 Mead Run";
    private string MeadThrow = "P1 Mead Throw";
    private string MeadThrowAction = "P1 Mead Throw Action";



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
    [SerializeField] private bool pickedUpSpear;
    [SerializeField] private bool pickedUpMead;

    private void Awake()
    {
        switch (player)
        {
            case Players.P1:
                PlayerIdle = "Player 1 Idle";
                PlayerRun = "Player1 Run";
                PlayerTakeOff = "Player 1 Take off";
                PlayerJump = "Player 1 Jump";
                PlayerLanding = "Player1Landing";
                PlayerPickup = "Player1 Box Pickup";
                PlayerHoldingIdle = "Player1 holdingIdle";
                PlayerHoldingRun = "Player 1 holding Run";
                PlayerPutDown = "Player 1 Put Down";
                PlayerBoxPlayerChargeThrow = "Player 1 Throw";
                PlayerThrowActionBoxPlayer = "Player 1 Throw Action";
                AxeIdle = "P1 Axe Idle";
                AxeJump = "P1 Axe Jump";
                AxeRun = "P1 Axe Run";
                AxeThrowAction = "P1 Axe Throw Action";
                AxeThrow = "P1 Axe Throw";
                SpearIdle = "P1 Spear idle";
                SpearJump = "P1 Spear Jump";
                SpearRun = "P1 Spear Run";
                SpearThrow = "P1 Spear Throw";
                SpearThrowAction = "P1 Spear Throw Action";
                MeadIdle = "P1 Mead Idle";
                MeadJump = "P1 Mead Jump";
                MeadRun = "P1 Mead Run";
                MeadThrow = "P1 Mead Throw";
                MeadThrowAction = "P1 Mead Throw Action";
                break;
            case Players.P2:
                PlayerIdle = "Player 2 Idle";
                PlayerRun = "Player 2 Run";
                PlayerTakeOff = "Player 2 Take Off";
                PlayerJump = "Player 2 jump";
                PlayerLanding = "Player 2 Landing";
                PlayerPickup = "Player 2 Pickup";
                PlayerHoldingIdle = "Player 2 holding Idle";
                PlayerHoldingRun = "Player 2 holding Run";
                PlayerPutDown = "Player 2 Put Down";
                PlayerBoxPlayerChargeThrow = "Player 2 Throw";
                PlayerThrowActionBoxPlayer = "Player 2 Throw Action";
                AxeIdle = "P2 Axe Idle";
                AxeJump = "P2 Axe Jump";
                AxeRun = "P2 Axe Run";
                AxeThrowAction = "P2 Axe Throw Action";
                AxeThrow = "P2 Axe Throw";
                SpearIdle = "P2 Spear idle";
                SpearJump = "P2 Spear Jump";
                SpearRun = "P2 Spear Run";
                SpearThrow = "P2 Spear Throw";
                SpearThrowAction = "P2 Spear Throw Action";
                MeadIdle = "P2 Mead Idle";
                MeadJump = "P2 Mead Jump";
                MeadRun = "P2 Mead Run";
                MeadThrow = "P2 Mead Throw";
                MeadThrowAction = "P2 Mead Throw Action";
                break;
        }
    }

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
        pickedUpSpear = Interact.pickedUpSpear;
        pickedUpMead = Interact.pickedUpMead;

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
    }



    public void Jump()
    {
        if (pickedupAxe)
        {
            ChangeAnimationState(AxeJump);
        }
        else if (pickedUpSpear)
        {
            ChangeAnimationState(SpearJump);
        }
        else if (pickedUpMead)
        {
            ChangeAnimationState(MeadJump);
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
        else if (pickedUpSpear)
        {
            ChangeAnimationState(SpearIdle);
        }
        else if (pickedUpMead)
        {
            ChangeAnimationState(MeadIdle);
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
        else if (pickedUpSpear)
        {
            ChangeAnimationState(SpearRun);
        }
        else if (pickedUpMead)
        {
            ChangeAnimationState(MeadRun);
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
            ChangeAnimationState(AxeThrow);
        }
        else if (pickedUpSpear)
        {
            ChangeAnimationState(SpearThrow);
        }
        else if (pickedUpMead)
        {
            ChangeAnimationState(MeadThrow);
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
        else if (pickedUpSpear)
        {
            ChangeAnimationState(SpearThrowAction);
        }
        else if (pickedUpMead)
        {
            ChangeAnimationState(MeadThrowAction);
        }
        else
        {
            ChangeAnimationState(PlayerThrowActionBoxPlayer);
        }   
        //if I'm holding a bxo play that throw anim etc
    }
    

  //public void PlayerHelpPose()
  //{
  //    ChangeAnimationState(HOld)
  //}

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
