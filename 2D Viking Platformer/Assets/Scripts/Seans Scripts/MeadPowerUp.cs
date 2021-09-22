using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeadPowerUp : MonoBehaviour
{
    [HideInInspector]
    public bool thrown = false;
    [SerializeField]
    private GameObject Mist;
    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject && Interact.thrownMug)
        {
            thrown = true;
            Instantiate(Mist, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);       
        }
    }


    public void TurnOff()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TurnOn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
