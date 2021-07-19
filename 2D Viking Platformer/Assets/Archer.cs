using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{

    public Rigidbody2D arrow;
    public Transform arrowInstantiate;
    private bool fired = false;



    void Update()
    {
        RaycastHit2D Vision = Physics2D.Raycast(arrowInstantiate.position, Vector2.right * transform.localScale, 7f);
        if(Vision.collider != null && Vision.collider.tag == "Player")
        {
            if (!fired)
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
        yield return new WaitForSeconds(3);
        fired = false;
    }

}
