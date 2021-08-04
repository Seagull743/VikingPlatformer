using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool Player1 = false;
    private bool Player2 = false;
    [SerializeField]
    private GameObject youWonCanvas;
    private bool loadscene;
    void Start()
    {
        loadscene = false;
        youWonCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Player1 && Player2)
        {
            if (!loadscene)
            {
                youWonCanvas.SetActive(true);
                Invoke("LoadScene", 2f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            Player1 = true;
        }

        if (collision.gameObject.name == "Player 2")
        {
            Player2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player1 = false;
        Player2 = false;
    }

    void LoadScene()
    {
        loadscene = true;
        SceneManager.LoadScene("MainMenu");
    }
}
