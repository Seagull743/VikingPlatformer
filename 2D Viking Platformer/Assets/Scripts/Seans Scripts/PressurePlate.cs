using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool opened = false;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private rune runeobject;
    [SerializeField]
    private DoorPressure DP;
    public List<GameObject> CollisionItems = new List<GameObject>();
    [SerializeField]
    private PressurePlate otherpressureplate;

    private void Update()
    {
        
        if(otherpressureplate != null)
        {
            if (CollisionItems.Count >= 1 || otherpressureplate.CollisionItems.Count >= 1)
            {
                if (!opened)
                {
                    opened = true;
                    //anim.SetBool("isActivated", true);
                    runeobject.ToggleRuneOn();
                    DP.OpenDoor();
                }
            }
            else if (CollisionItems.Count <= 0 && otherpressureplate.CollisionItems.Count <= 0)
            {
                if (opened)
                {
                    opened = false;
                    //anim.SetBool("isActivated", false);
                    runeobject.ToggleRuneOff();
                    DP.CloseDoor();
                }
            }
        }
        else if(otherpressureplate == null)
        {
            if (CollisionItems.Count >= 1)
            {
                if (!opened)
                {
                    opened = true;
                    //anim.SetBool("isActivated", true);
                    runeobject.ToggleRuneOn();
                    DP.OpenDoor();
                }
            }
            else if (CollisionItems.Count <= 0)
            {
                if (opened)
                {
                    opened = false;
                    //anim.SetBool("isActivated", false);
                    runeobject.ToggleRuneOff();
                    DP.CloseDoor();
                }
            }
        }
        
    }

  private void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
      {
          CollisionItems.Add(other.gameObject);
          anim.SetBool("isActivated", true);
      }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
      if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
      {
          CollisionItems.Remove(other.gameObject);
          anim.SetBool("isActivated", false);
      }
  } 
}
