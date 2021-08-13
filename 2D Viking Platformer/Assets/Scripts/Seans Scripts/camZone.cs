using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camZone : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;
    
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
