using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


public class GearBox : MonoBehaviour
{
    public GameObject ExitDoor;
    public GameObject GearLeft, GearRight, GearMiddle;
    public GameObject lock1, lock2, lock3;
   private int gearsNeeded=3;
   private bool isDoorOpen=false;

   public Animator counterClock;
   public Animator clockwiseLeft;
   public Animator clockwiseRight;
   public Animator exitOpen;
   public Animator unlocked1;
   public Animator unlocked2;
   public Animator unlocked3;

 private void Start()
    {
        // makes the gears in the gear box not visalble to begin with
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

    void DoorOpen() // this starts the coroutine when all gears are sloted into the gear box along with the animations and sounds.
    {
       isDoorOpen=true;
        StartCoroutine(gearSound());
        GearLeft.SetActive(true);
        GearMiddle.SetActive(true);
        GearRight.SetActive(true);
        counterClock.SetTrigger("gearRotation");
        clockwiseRight.SetTrigger("gearTurn");
        clockwiseLeft.SetTrigger("gearTurn");
        unlocked1.SetTrigger("unlock");
        unlocked2.SetTrigger("unlock");
        unlocked3.SetTrigger("unlock");
       exitOpen.SetTrigger("exitDoorOpen"); 
       SoundManager.Play(SoundType.OPEN);
    }

    IEnumerator gearSound() // I used a coroutine because a regular while loop crashes the whole program.
    {
        while (isDoorOpen)
        {
            
            SoundManager.Play(SoundType.GEAR);
            yield return new WaitForSeconds(2f);
        }
        
    }
}
