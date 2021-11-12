using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool Player1 = false;
    private bool Player2 = false;
    private bool doorReached = false;
   
    public int levelToUnlock;
    [SerializeField] LevelNamesData levelNamesData;
    public SceneFader sceneFader;
    [SerializeField]
    private GameObject waitingplayertext;
    [SerializeField]
    private GameObject youWonCanvas;
    public GM gameManager;
    [SerializeField]
    private Animator anim;

    void Start()
    {
        waitingplayertext.SetActive(false);
        youWonCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Player1 && Player2 && !doorReached)
        {
            waitingplayertext.SetActive(false);
            anim.SetTrigger("open");
            StartCoroutine(Sceneloader());
            doorReached = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !doorReached)
        {
            waitingplayertext.SetActive(true);
        }

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
        if (collision.gameObject.tag == "Player" && !doorReached)
        {
            waitingplayertext.SetActive(false);
        }
        Player1 = false;
        Player2 = false;
    }
    public void UnlockLevel(int _levelToUnlock)
    {
        Debug.Log("LevelUnlock = " + _levelToUnlock);
        PlayerPrefs.SetInt("levelReached", _levelToUnlock);
        sceneFader.FadeTo(levelNamesData.levelNames[_levelToUnlock - 1]);
    }


    IEnumerator Sceneloader()
    {
        yield return new WaitForSeconds(2);
        UnlockLevel(levelToUnlock);
        youWonCanvas.SetActive(true);
        gameManager.PlayerMovementOff();
        Invoke("Level01", 2f);
        Time.timeScale = 0f;
    }
}
