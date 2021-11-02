using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool playerAlive = true;
    public GM gm;
   
    public void PlayerDamaged()
    {
        if (gameObject.GetComponent<Interact>().isHolding)
        {
            gameObject.GetComponent<Interact>().Drop();
        }
        playerAlive = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,10,0);
        this.gameObject.GetComponent<Interact>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        Invoke("ResetGame", 2.5f);
    }


    private void ResetGame()
    {
        if(gameObject.name == "Player 1")
        {
            gm.SpawnPlayer1();
        }

        if(gameObject.name == "Player 2")
        {
            gm.SpawnPlayer2();
        }
        this.gameObject.GetComponent<Interact>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        this.gameObject.GetComponent<PlayerController>().enabled = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

 //private void OnCollisionEnter2D(Collision2D collision)
 //{
 //    if(collision.gameObject.tag == "Arrow")
 //    {
 //        PlayerDamaged();
 //    }
 //}

}
