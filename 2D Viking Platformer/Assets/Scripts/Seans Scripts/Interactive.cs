
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{

    [SerializeField]
    private Transform holdCheck;
    [SerializeField]
    private Transform grabDetect;
    [SerializeField]
    private Transform placeChecker;
    [SerializeField]
    private Transform holdLocation;
    [SerializeField]
    private Transform dropLocation;
    [SerializeField]
    private float raydist;
    [SerializeField]
    private KeyCode interact;
    [SerializeField]
    private KeyCode throwing;
    public bool isHolding = false;

    [SerializeField]
    private float throwforce;

    private RaycastHit2D grabcheck;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interact))
        {
            if (!isHolding)
            {
                grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, raydist);
                //box
                if (grabcheck.collider != null && grabcheck.collider.tag == "Box")
                {   
                    grabcheck.collider.gameObject.transform.parent = holdLocation;
                    grabcheck.collider.gameObject.transform.position = holdLocation.position;
                    grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    isHolding = true;
                }
                //player
                else if (grabcheck.collider != null && grabcheck.collider.tag == "Player")
                {
                    if(grabcheck.collider.gameObject.GetComponent<Interactive>().isHolding == false)
                    {
                        var Rb = grabcheck.collider.gameObject.GetComponent<Rigidbody2D>();
                        Rb.velocity = Vector3.zero;
                        grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        grabcheck.collider.gameObject.transform.parent = holdLocation;
                        grabcheck.collider.gameObject.transform.position = holdLocation.position;
                        Rb.isKinematic = true;
                        grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = false;
                        grabcheck.collider.gameObject.GetComponent<PlayerController>().enabled = false;
                        isHolding = true;
                    }                
                }
                //Lever
                else if(grabcheck.collider != null && grabcheck.collider.tag == "Lever")
                {
                    grabcheck.collider.gameObject.GetComponent<Lever>().LeverToggle();
                }
            }
            else if (isHolding)
            {
                RaycastHit2D grabRay = Physics2D.Raycast(holdCheck.position, Vector2.up * transform.localScale, raydist);
                if ((grabRay.collider != null && grabRay.collider.tag == "Box" || grabRay.collider != null && grabRay.collider.tag == "Player"))
                {
                    RaycastHit2D placeCheck = Physics2D.Raycast(placeChecker.position, Vector2.right * transform.localScale, raydist);
                    if(placeCheck.collider == null)
                    {
                        grabcheck.collider.gameObject.transform.parent = null;
                        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                        grabcheck.collider.gameObject.transform.SetPositionAndRotation(dropLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        isHolding = false;
                        if (grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
                        {
                            grabcheck.collider.gameObject.GetComponent<PlayerController>().enabled = true;
                            grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = true;
                        }
                    }             
                }
                else
                {
                    isHolding = false;
                }
            }
        }
        if (Input.GetKeyDown(throwing) && isHolding)
        {
            grabcheck.collider.gameObject.transform.parent = null;
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false; 
            grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwforce;
            grabcheck.collider.gameObject.transform.SetPositionAndRotation(holdLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            isHolding = false;
            if(grabcheck.collider.gameObject.GetComponent<PlayerController>() != null)
            {
                grabcheck.collider.gameObject.GetComponent<PlayerController>().controller();
                grabcheck.collider.gameObject.GetComponent<Interactive>().enabled = true;
            }
        }

    }
}
