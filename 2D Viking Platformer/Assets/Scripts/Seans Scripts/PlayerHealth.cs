using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("MainMenu");
    }

 //private void OnCollisionEnter2D(Collision2D collision)
 //{
 //    if(collision.gameObject.tag == "Arrow")
 //    {
 //        PlayerDamaged();
 //    }
 //}

}
