using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DanScene");
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

    public void Level2()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }
}
