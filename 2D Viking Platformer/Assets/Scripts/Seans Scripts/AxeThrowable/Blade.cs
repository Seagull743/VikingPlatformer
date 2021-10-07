using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    private Axe axe;
    
    // Start is called before the first frame update
    void Start()
    {
        axe = GetComponentInParent<Axe>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && axe.thrown)
        {
            if (collision.gameObject.GetComponent<MeleeDude>() != null)
            {
                collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
                axe.AxeStop();
            }
            else if (collision.gameObject.GetComponent<Archer>() != null)
            {
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
                axe.AxeStop();
            }
        }
        else if (collision.gameObject.tag != "Enemy" && axe.thrown)
        {
            axe.ThrownToggle();
        }
    }

}
