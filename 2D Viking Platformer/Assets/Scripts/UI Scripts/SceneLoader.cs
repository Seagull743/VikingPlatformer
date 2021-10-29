using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    //public string levelToLoad = "Level 1";

    public GM gameManager;
    
    public SceneFader sceneFader;
    public void PlayGame()
    {
        //sceneFader.FadeTo(levelToLoad);

    }

    public void BacktoMenu()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        gameManager.PlayerMovementOn();
    }

    public void RestartGame()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }


}
