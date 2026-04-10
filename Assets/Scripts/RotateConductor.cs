using UnityEngine;

public class RotateConductor : MonoBehaviour {
  public Conductor gear1;
  public Conductor gear2;

  public void Grab() {
    SoundManager.Play(SoundType.BUTTON);
    gear1.ChangeState(ConductorState.TURNING);
    gear2.ChangeState(ConductorState.TURNING);
  }
}