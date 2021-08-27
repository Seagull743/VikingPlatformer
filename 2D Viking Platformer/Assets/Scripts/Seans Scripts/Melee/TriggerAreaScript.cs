using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaScript : MonoBehaviour
{
    private MeleeDude enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<MeleeDude>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            enemyParent.target = collision.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
