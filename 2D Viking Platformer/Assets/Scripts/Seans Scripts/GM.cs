using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public GameObject YouLoseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        YouLoseCanvas.SetActive(false);
 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



    public void PlayerdeathCanvas()
    {
        YouLoseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
