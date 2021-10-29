using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    public string nextLevel = "Level 2";
    public int levelToUnlock = 2;

    public PlayerController[] players;
    public SceneFader sceneFader;
    [SerializeField]
    private Transform spawnPoint;

    public PlayerHealth player1;
    public PlayerHealth player2;

    [SerializeField]
    private GameObject[] BreakablePlatform;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SpawnPlayer()
    {
        if (!player1.playerAlive || !player2.playerAlive)
        {
            Rigidbody2D playerRb = player1.GetComponent<Rigidbody2D>();
            playerRb.transform.position = spawnPoint.position;
            player1.playerAlive = true;
        }
       
       // if (!player2.playerAlive)
       // {
         //   Rigidbody2D playerRb = player2.GetComponent<Rigidbody2D>();
        //    playerRb.transform.position = spawnPoint.position;
       //     player2.playerAlive = true;
       // }

    }

    public void Level01()
    {
        Debug.Log("LevelUnlock");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void PlayerMovementOff()
    {
        foreach(PlayerController pc in players)
        {
            pc.GetComponent<PlayerController>().enabled = false;
        }
    }





    public void PlayerMovementOn()
    {
        foreach (PlayerController pc in players)
        {
            pc.GetComponent<PlayerController>().enabled = true;
        }
    }
}
