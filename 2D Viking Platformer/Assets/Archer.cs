using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{

    public Rigidbody2D arrow;
    public Transform arrowInstantiate;
    private bool fired = false;
    private bool stunned = false;



    void Update()
    {
        RaycastHit2D Vision = Physics2D.Raycast(arrowInstantiate.position, Vector2.right * transform.localScale, 3.5f);
        if(Vision.collider != null && Vision.collider.tag == "Player")
        {
            if (!fired && !stunned)
            {
                StartCoroutine(ArrowFire());
            }      
        }
    }


    IEnumerator ArrowFire()
    {
        fired = true;
        Rigidbody2D ArrowInstance;
        ArrowInstance = Instantiate(arrow, arrowInstantiate.position, arrowInstantiate.rotation) as Rigidbody2D;
        ArrowInstance.AddForce(arrowInstantiate.right * 350f);
        yield return new WaitForSeconds(3f);
        fired = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            StartCoroutine(Stunned());
        }
    }

    IEnumerator Stunned()
    {
        stunned = true;
        Debug.Log(stunned);
        yield return new WaitForSeconds(5);
        stunned = false;
        Debug.Log(stunned);
    }

}
