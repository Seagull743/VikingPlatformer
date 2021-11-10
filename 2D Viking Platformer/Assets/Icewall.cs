using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icewall : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D BigIceWall;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ColliderToggle()
    {
        StartCoroutine(ColliderStartToggle());
    }


    IEnumerator ColliderStartToggle()
    {
        BigIceWall.enabled = false;
        yield return new WaitForSeconds(0.5f);
        BigIceWall.enabled = true;
    }
}
