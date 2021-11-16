using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalScript : MonoBehaviour
{
    private bool Player1 = false;
    private bool Player2 = false;
    private bool doorReached = false;

    public SceneFader sceneFader;
    [SerializeField]
    private GameObject waitingplayertext;
    public GM gameManager;
    [SerializeField]
    private ParticleSystem portalEffect;



    void Start()
    {
        waitingplayertext.SetActive(false);
        portalEffect.Stop();
    }

    private void Update()
    {
        if (Player1 && Player2 && !doorReached)
        {
            
            waitingplayertext.SetActive(false);
            StartCoroutine(Sceneloader());
            doorReached = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !doorReached)
        {
            waitingplayertext.SetActive(true);
            portalEffect.Play();
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
   
    public void LoadNextScene()
    {
        Debug.Log("Next Level");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Sceneloader()
    {
        yield return new WaitForSeconds(1);
        LoadNextScene();
    }
}
