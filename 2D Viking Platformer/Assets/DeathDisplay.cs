using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathDisplay : MonoBehaviour
{
    public Text arnesdeathCounter;
    public Text ulfsdeathCounter;
    public GM gamemanager;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        Debug.Log("Hello");
        arnesdeathCounter.text = gamemanager.arnedeathcounter.ToString();
        ulfsdeathCounter.text = gamemanager.ulfdeathcounter.ToString();
    }
}
