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
    public string PlayerBoxPlayerThrow = "Player 1 Throw";
    public string PlayerThrowAction = "Player 1 Throw Action";


    public bool isholding = false;


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
        if (!isholding)
        ChangeAnimationState(PlayerJump);
    }

    public void Idle()
    {
        if(!isholding)
        ChangeAnimationState(PlayerIdle);
    }

    public void Run()
    {
        if(!isholding)
        ChangeAnimationState(PlayerRun);
    }

    public void Pickup()
    {
        ChangeAnimationState(PlayerPickup);
        isholding = true;
    }

    public void HoldingIdle()
    {
        isholding = true;
        ChangeAnimationState(PlayerHoldingIdle);   
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
