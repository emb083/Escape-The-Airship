using UnityEngine;

public class RotateConductor : MonoBehaviour {
  public Gear gear1;
  public Gear gear2;

  public void Grab() {
    SoundManager.Play(SoundType.BUTTON);
    gear1.ChangeState(ConductorState.TURNING);
    gear2.ChangeState(ConductorState.TURNING);
  }
}