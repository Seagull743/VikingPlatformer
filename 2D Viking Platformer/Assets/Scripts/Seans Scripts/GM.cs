using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    public PlayerController[] players;
    public SceneFader sceneFader;
    [SerializeField]
    private Transform spawnPoint1;
    [SerializeField]
    private Transform spawnPoint2;

    public PlayerHealth player1;
    public PlayerHealth player2;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SpawnPlayer1()
    {
        if (!player1.playerAlive)
        {
            Rigidbody2D playerRb = player1.GetComponent<Rigidbody2D>();
            playerRb.transform.position = spawnPoint1.position;
            player1.playerAlive = true;
        }
        
    }

    public void SpawnPlayer2()
    {
        if (!player2.playerAlive)
        {
            Rigidbody2D playerRb = player2.GetComponent<Rigidbody2D>();
            playerRb.transform.position = spawnPoint2.position;
            player2.playerAlive = true;
        }
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
