using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    private BreakableIcePlatform IcePlatform;
    private bool called = false;

    private void Start()
    {
        IcePlatform = GetComponentInChildren<BreakableIcePlatform>();
    }

    private void Update()
    {
        if (IcePlatform.platformDestroyed && !called)
        {
            called = true;
            Invoke("RestartIcePlatform", 10f);

        }
    }


    public void RestartIcePlatform()
    {
        called = false;
        IcePlatform.RespawnIce();
    }
}
