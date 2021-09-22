using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private bool playerAlive = true;
    public GM gm;


   
    public void PlayerDamaged()
    {
        playerAlive = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,10,0);
        this.gameObject.GetComponent<Interact>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        Invoke("ResetGame", 2f);
    }


    private void ResetGame()
    {
        gm.PlayerdeathCanvas();
    }

 //private void OnCollisionEnter2D(Collision2D collision)
 //{
 //    if(collision.gameObject.tag == "Arrow")
 //    {
 //        PlayerDamaged();
 //    }
 //}

}
