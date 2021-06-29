using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool playerAlive = true;



    public void PlayerDamaged()
    {
        playerAlive = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,10,0);
        this.gameObject.GetComponent<Interactive>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        Invoke("ResetGame", 2);
    }


    private void ResetGame()
    {
        Debug.Log("Game Over");
    }

}
