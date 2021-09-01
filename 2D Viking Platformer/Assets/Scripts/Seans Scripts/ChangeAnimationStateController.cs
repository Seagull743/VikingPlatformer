using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationStateController : MonoBehaviour
{


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

    //isGrounded && canJump

    private PlayerController PC;
    private Interactive Interact;
   
    //Bools PlayerController
    private bool isrunning;
    private bool isGrounded;
    private bool CanJump;
    private bool isjumping;

    //Bools Interactive
    private bool isholding;
    private bool isThrowing;
    private bool isthrown;
    private bool pickuped;
    private bool putDown;



   //  [HideInInspector] public bool pickup = false;
   //  [HideInInspector] public bool isHolding = false;
   //  [HideInInspector] public bool isthrowing = false;
   //  [HideInInspector] public bool putdown = false;
   //  [HideInInspector] public bool Thrown = false;


    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<PlayerController>();
        Interact = GetComponent<Interactive>();
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
        pickuped = Interact.pickup;
        putDown = Interact.putdown;

        //Running Animations
        if (isrunning && isGrounded && !pickuped && !isholding)
        {
            Run();
        }
        else if (isrunning && isGrounded && isholding && !isThrowing) //pickuped
        {
            HoldingRun();
        }
        else if (!isrunning && isGrounded && !pickuped && !isholding)
        {
            Idle();
        }
        else if (!isrunning && isGrounded && isholding && !putDown && !isThrowing) //pickuped
        {
            HoldingIdle();
        }

        //Jumping Animations
        if (!isGrounded)
        {
            Jump();
        }

        //PickUp and dropping
        if(!isholding && pickuped && isGrounded)
        {
            Pickup();
        }
        else if(isholding && pickuped && isGrounded && putDown)
        {
            PutDown();
        }

        //Throwing
        if(isholding && isThrowing && !isthrown)
        {
            ChargeThrow();
        }
        else if(isholding && isThrowing && isthrown)
        {
            Thrown();
        }
    }
    

    public void Jump()
    {
      
        ChangeAnimationState(PlayerJump);
    }

    public void Idle()
    {
        ChangeAnimationState(PlayerIdle);   
    }

    public void HoldingIdle()
    {
        ChangeAnimationState(PlayerHoldingIdle);
    }

    public void Run()
    {
         ChangeAnimationState(PlayerRun);
    }

    public void HoldingRun()
    {
        ChangeAnimationState(PlayerHoldingRun);
    }
    public void ChargeThrow()
    {
        ChangeAnimationState(PlayerBoxPlayerChargeThrow);
    }
    
    public void Thrown()
    {
        ChangeAnimationState(PlayerThrowActionBoxPlayer);
    }
    
    public void Pickup()
    {
        ChangeAnimationState(PlayerPickup);
    }

    public void PutDown()
    {
        ChangeAnimationState(PlayerPutDown);
    }


   // Animation Event on Put Down
    public void IsHoldingFalse()
    {
        isholding = false;
        pickuped = false;
    }

    //Animation Event on PickUp
   // public void IsHoldingTrue()
   // {
    //    isholding = true;
   // }

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
