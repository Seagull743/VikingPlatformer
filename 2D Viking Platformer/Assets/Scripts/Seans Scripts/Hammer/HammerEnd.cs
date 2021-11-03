using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerEnd : MonoBehaviour
{

    public Hammer hammer;

    private void Update()
    {
     
            if (hammer.canKill)
            {
                gameObject.layer = 15;
            }
            else if (hammer.canKill == false)
            {
                gameObject.layer = 21;
            }

        }


        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && hammer.canKill)
        {
            
            if (collision.gameObject.GetComponent<MeleeDude>() != null)
            {
                hammer.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                hammer.stopAnimation();
                hammer.ThrownToggle();
                hammer.CanStunHammerToggle();
                collision.gameObject.GetComponent<MeleeDude>().EnemyStun();
            }
            else if (collision.gameObject.GetComponent<Archer>() != null)
            {
                hammer.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                hammer.stopAnimation();
                hammer.ThrownToggle();
                hammer.CanStunHammerToggle();
                collision.gameObject.GetComponent<Archer>().Stunned();
            }
        }
        else if(collision.gameObject.tag == "IceWallHammer" && hammer.thrown)
        {
            collision.gameObject.GetComponent<BreakIceWallHammer>().IceBreak();
            hammer.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            hammer.stopAnimation();
            hammer.ThrownToggle();
            hammer.CanStunHammerToggle();
        }
        else if (collision.gameObject.tag != "Enemy" && hammer.thrown && collision.gameObject.tag != "wall" && collision.gameObject.tag != "Box")
        {
            hammer.ThrownToggle();
            hammer.Invoke("CanStunHammerToggle", 0.5f);
        }

    }

}
