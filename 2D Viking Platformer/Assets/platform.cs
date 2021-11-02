using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
   
    [SerializeField]
    private GameObject IcePlatform;
    [SerializeField]
    private GameObject IcePlatformCopy;
    private bool called = false;

    private void Update()
    {       
        if(IcePlatform == null && !called)
        {
            called = true;
            Invoke("SpawnIcePlatform", 5f);        
        }
    }

    public void SpawnIcePlatform()
    {
        Instantiate(IcePlatformCopy, gameObject.transform.position, gameObject.transform.rotation);
        called = false; 
    }


}
