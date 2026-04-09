using UnityEngine;

public class RotateGear : MonoBehaviour {
  public Gear gear1;
  public Gear gear2;

  public void Grab() {
    gear1.ChangeState(GearState.TURNING);
    gear2.ChangeState(GearState.TURNING);
  }
}