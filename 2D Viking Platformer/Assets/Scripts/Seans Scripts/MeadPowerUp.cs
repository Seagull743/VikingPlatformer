using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeadPowerUp : MonoBehaviour
{
    [HideInInspector]
    public bool thrown = false;
    [SerializeField]
    private GameObject Mist;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thrown)
        {
            transform.Rotate(0, 0, -2);
        }
        else if (!thrown)
        {
            transform.Rotate(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject && Interactive.thrownMug)
        {
            thrown = false;
            Instantiate(Mist, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);       
        }
    }
}
