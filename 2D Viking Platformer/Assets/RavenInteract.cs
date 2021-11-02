using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenInteract : MonoBehaviour
{
    private Dialog dialogManager;
    private bool pressed = false;


    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GetComponentInChildren<Dialog>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.tag == "Player") 
        {
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl))
            {
                if (!pressed)
                {
                    StartCoroutine(PressCoolDown());
                }
            }
        }
    }


    IEnumerator PressCoolDown()
    {
        pressed = true;
        dialogManager.TypeStarter();
        yield return new WaitForSeconds(10f);
        pressed = false;
    }


}
