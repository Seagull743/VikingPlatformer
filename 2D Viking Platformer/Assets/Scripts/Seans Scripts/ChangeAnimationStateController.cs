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


    private bool isholding = false;
    private bool isrunning = false;
    private bool ChargingThrow = false;
    private bool thrownAction = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
      
        ChangeAnimationState(PlayerJump);
    }

    public void Idle()
    {
        isrunning = false;
        if (isholding)
        {
            ChangeAnimationState(PlayerHoldingIdle);
        }
        else
        {
            ChangeAnimationState(PlayerIdle);
        }       
    }
    public void Run()
    {
        isrunning = true;
        if (isholding)
        {
            ChangeAnimationState(PlayerHoldingRun);
        }
        else
        {
            ChangeAnimationState(PlayerRun);
        }   
    }

   
    public void ChargeThrow()
    {
        ChargingThrow = true;
        if(isholding && ChargingThrow)
        {
            ChangeAnimationState(PlayerBoxPlayerChargeThrow);
        }
    }
    
    public void Thrown()
    {
        thrownAction = true;
        if (isholding && ChargingThrow && thrownAction)
        {
            ChangeAnimationState(PlayerThrowActionBoxPlayer);
        }
    }
    
    public void Pickup()
    {
        isholding = true;
        ChangeAnimationState(PlayerPickup);
    }

    public void PutDown()
    {
        ChangeAnimationState(PlayerPutDown);
    }



   
    //Animation Event on Put Down
    public void IsHoldingFalse()
    {
        isholding = false;
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
