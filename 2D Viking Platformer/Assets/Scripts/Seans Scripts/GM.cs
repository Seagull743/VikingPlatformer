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
    [SerializeField]
    private GameObject SpawnPartical;

    public PlayerHealth player1;
    public PlayerHealth player2;

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
            player1.GetComponent<Interact>().enabled = false;
            player1.GetComponent<PlayerController>().enabled = false;
            Rigidbody2D playerRb = player1.GetComponent<Rigidbody2D>();
            playerRb.transform.position = spawnPoint1.position;
            player1.playerAlive = true;
            Invoke("MovementSpawnPlayer1", 3f);
        }
        
    }

    public void SpawnPlayer2()
    {
        if (!player2.playerAlive)
        {
            player2.GetComponent<Interact>().enabled = false;
            player2.GetComponent<PlayerController>().enabled = false;
            Rigidbody2D playerRb = player2.GetComponent<Rigidbody2D>();
            playerRb.transform.position = spawnPoint2.position;
            player2.playerAlive = true;
            Invoke("MovementSpawnPlayer2", 3f);

        }
    }

    private void MovementSpawnPlayer2()
    {
            player2.GetComponent<SpriteRenderer>().enabled = true;
            player2.GetComponent<Interact>().enabled = true;
            player2.GetComponent<PlayerController>().enabled = true;
    }

    private void MovementSpawnPlayer1()
    {
            player1.GetComponent<SpriteRenderer>().enabled = true;
            player1.GetComponent<Interact>().enabled = true;
            player1.GetComponent<PlayerController>().enabled = true;
    }

    public void SpawnParticalBeam1()
    {
        Instantiate(SpawnPartical, player1.BifrostLocation.position, player1.BifrostLocation.rotation);
    }

    public void SpawnParticalBeam2()
    {
        Instantiate(SpawnPartical, player2.BifrostLocation.position, player2.BifrostLocation.rotation);
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
