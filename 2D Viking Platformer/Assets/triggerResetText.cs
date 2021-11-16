using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerResetText : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionReset;
    private static bool calledReset = false;

    private void Start()
    {
        instructionReset.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Box" && !calledReset)
        {
            instructionReset.SetActive(true);
        }
    }

}
