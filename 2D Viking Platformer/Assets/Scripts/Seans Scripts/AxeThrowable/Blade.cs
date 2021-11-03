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

    private void Update()
    {
        if (axe.thrown)
        {
            gameObject.layer = 15;
        }
        else if(axe.thrown == false)
        {
            gameObject.layer = 21;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && axe.thrown)
        {
            if (collision.gameObject.GetComponent<MeleeDude>() != null)
            {
                axe.HitEnemy();
                collision.gameObject.GetComponent<MeleeDude>().EnemyDieing();
            }
            else if (collision.gameObject.GetComponent<Archer>() != null)
            {
                axe.HitEnemy();
                collision.gameObject.GetComponent<Archer>().EnemyDieing();
            }
        }
        else if (collision.gameObject.tag != "Enemy" && axe.thrown && collision.gameObject.tag != "wall" && collision.gameObject.tag != "Box")
        {
            axe.stopAnimation();
            axe.ThrownToggle();
        }

    }

}
