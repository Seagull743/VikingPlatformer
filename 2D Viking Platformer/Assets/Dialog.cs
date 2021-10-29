using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    [SerializeField]
    private GameObject speechbubble1;
    private int index;
    [SerializeField]
    private GameObject InteractText;


    private void Start()
    {
        speechbubble1.SetActive(false);
        InteractText.SetActive(false);
    }

    public void TypeStarter()
    {
        StartCoroutine(Type());
        Invoke("DeleteText", 13);
    }

    public void Interaction()
    {
        StartCoroutine(InteractTextToggle());
    }

    IEnumerator InteractTextToggle()
    {
        InteractText.SetActive(true);
        yield return new WaitForSeconds(4);
        InteractText.SetActive(false);
    }

    IEnumerator Type()
    {
        speechbubble1.SetActive(true);
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void DeleteText()
    {
        textDisplay.text = null;
        speechbubble1.SetActive(false);
    }

}

