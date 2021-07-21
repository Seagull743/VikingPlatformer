using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private MeleeDude enemyParent;
    private bool inRange;
    //private Aimator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<MeleeDude>();
        //get anim
    }

    private void Update()
    {
        //anim state current thing
        if (inRange)
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }

}
