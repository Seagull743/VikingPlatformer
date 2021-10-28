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
    public GM gameManager;
    public SceneLoader sceneLoader;
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
                StartCoroutine(Sceneloader());
                
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
    public void GameisEnd()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Debug.Log("YOU WIN GAME");
        }
        else
        {
            sceneLoader.NextLevel();
        }
    }
    void LoadScene()
    {
        loadscene = true;
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("MainMenu");
    }
    IEnumerator Sceneloader()
    {
        yield return new WaitForSeconds(2);
        youWonCanvas.SetActive(true);
        gameManager.PlayerMovementOff();
        gameManager.Level01();
        Invoke("LoadScene", 2f);
        GameisEnd();
        Time.timeScale = 0f;
    }
}
