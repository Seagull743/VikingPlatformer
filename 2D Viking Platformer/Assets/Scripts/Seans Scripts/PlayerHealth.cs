using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool playerAlive = true;
    public GM gm;
    public Transform BifrostLocation;
   
    public void PlayerDamaged()
    {
        if (gameObject.GetComponent<Interact>().isHolding)
        {
            gameObject.GetComponent<Interact>().Drop();
        }
        playerAlive = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,10,0);
        this.gameObject.GetComponent<Interact>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        Invoke("ResetGame", 1f);
    }

    public void PlayerDrown()
    {
        if (gameObject.GetComponent<Interact>().isHolding)
        {
            gameObject.GetComponent<Interact>().Drop();
        }
        playerAlive = false;
        this.gameObject.GetComponent<Interact>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        Invoke("ResetGame", 1f);
    }

    public void ResetGame()
    {
        if(gameObject.name == "Player 1")
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            //gm.SpawnParticalBeam1();
            //gm.arnedeathcounter ++;
            gm.SpawnPlayer1();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if(gameObject.name == "Player 2")
        {
            //gm.ulfdeathcounter ++;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //gm.SpawnParticalBeam2();
            gm.SpawnPlayer2();
        }
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
