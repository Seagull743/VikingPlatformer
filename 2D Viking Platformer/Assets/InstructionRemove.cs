using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionRemove : MonoBehaviour
{
    [SerializeField]
    private GameObject player1Instructions;
    [SerializeField]
    private GameObject player2Instructions;
    private bool P1Reached = false;
    private bool P2Reached = false;

    private void Update()
    {
        if(P1Reached && P2Reached)
        {
            player1Instructions.SetActive(false);
            player2Instructions.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player 1")
        {
            P1Reached = true;
        }

        if (collision.gameObject.name == "Player 2")
        {
            P2Reached = true;
        }
    }


}
