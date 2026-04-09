using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GearBox : MonoBehaviour
{
    public GameObject ExitDoor;
    public GameObject GearLeft, GearRight, GearMiddle;

   private int gearsNeeded=3;
   private bool isDoorOpen=false;

   public Animator counterClock;
   public Animator clockwiseLeft;
   public Animator clockwiseRight;

 private void Start()
    {
        
        GearLeft.SetActive(false);
        GearMiddle.SetActive(false);
        GearRight.SetActive(false);
    }
   void Grab()
    {
        if (isDoorOpen) // safty check to make sure the door opens once and gears are not removed again.
        {
            return;
        }
        if (Inventory.Instance.NumberOfGears() >= gearsNeeded) // if the right number of gears is in the player inventory when they interact with the gear box the gears are removed and the door opens.
        {
            Inventory.Instance.RemoveGear(gearsNeeded);
            DoorOpen();
        } else
        {
            Debug.Log("I need more gears to get this working.");
        }
    }

    void DoorOpen()
    {
       isDoorOpen=true;
        
        GearLeft.SetActive(true);
        GearMiddle.SetActive(true);
        GearRight.SetActive(true);
        counterClock.SetTrigger("gearRotation");
        clockwiseRight.SetTrigger("gearTurn");
        clockwiseLeft.SetTrigger("gearTurn");
       ExitDoor.SetActive(false); // add door open animation here. 
    }
}
