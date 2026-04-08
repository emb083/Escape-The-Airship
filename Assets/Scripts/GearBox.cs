using UnityEngine;

public class GearBox : MonoBehaviour
{
    public GameObject ExitDoor;
   private int gearsNeeded=3;
   private bool isDoorOpen=false;

   void Grab()
    {
        if (isDoorOpen) // safty check to make sure the door opens once and gears are not removed again.
        {
            return;
        }
        if (Inventory.Instance.NumberOfGears() >= gearsNeeded) // if the right number of gears is in the player inventory when they interact with the gear box the gears are removed and the door opens.
        {
            Inventory.Instance.RemoveGear(gearsNeeded);
            doorOpen();
        } else
        {
            Debug.Log("I need more gears to get this working.");
        }
    }

    void doorOpen()
    {
       isDoorOpen=true;
       ExitDoor.SetActive(false); // add door open animation here. 
    }
}
