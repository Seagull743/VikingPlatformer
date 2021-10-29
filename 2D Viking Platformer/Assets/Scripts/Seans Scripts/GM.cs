using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    public PlayerController[] players;
    public SceneFader sceneFader;

    // Start is called before the first frame update
    void Start()
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
