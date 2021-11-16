using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCredits : MonoBehaviour
{
    public GameObject Credits;
    
    // Start is called before the first frame update
    void Start()
    {
        Credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Credits.SetActive(true);
        }
    }


}
