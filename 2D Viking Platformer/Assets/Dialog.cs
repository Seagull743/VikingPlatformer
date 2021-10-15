using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;


    public void TypeStarter()
    {
        StartCoroutine(Type());
        Invoke("DeleteText", 13);
    }

    IEnumerator Type()
    {

        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void DeleteText()
    {
        textDisplay.text = null;
    }

}

