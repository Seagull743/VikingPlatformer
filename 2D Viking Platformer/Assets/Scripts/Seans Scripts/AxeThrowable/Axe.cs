using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public bool thrown = false;
    private Rigidbody2D rb;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject itemHighlight;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (thrown)
        {
            anim.SetBool("thrown", true);
        }
        else
        {
            anim.SetBool("thrown", false);
        }
    }

    public void KinematicToggle()
    {
        rb.drag = 0;
        rb.isKinematic = false;
        rb.freezeRotation = false;
    }

    public void HitEnemy()
    {
        thrown = false;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        rb.isKinematic = true;
        Invoke("KinematicToggle", 0.3f);
    }

    public void ThrownToggle()
    {
        thrown = false;
        rb.isKinematic = true;
        rb.freezeRotation = true;
        rb.velocity = Vector3.zero;
    }

    public void stopAnimation()
    {
        anim.SetBool("thrown", false);
    }

    public void TurnOff()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        itemHighlight.SetActive(false);
    }

    public void FreezeRotation()
    {
        rb.freezeRotation = true;
    }

    public void AxeStop()
    {
        thrown = false;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        rb.isKinematic = true;
        Invoke("KinematicToggle", 0.3f);
    }

    public void TurnOn()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        itemHighlight.SetActive(true);
    }
}


