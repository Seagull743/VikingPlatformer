using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public GameObject YouLoseCanvas;

    public string nextLevel = "Level 2";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    // Start is called before the first frame update
    void Start()
    {
        YouLoseCanvas.SetActive(false);
 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



    public void PlayerdeathCanvas()
    {
        YouLoseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Level01()
    {
        Debug.Log("LevelUnlock");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }


}
