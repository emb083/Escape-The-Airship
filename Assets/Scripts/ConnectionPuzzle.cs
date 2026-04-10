using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionPuzzle : MonoBehaviour {
  // set in inspector
  public List<Conductor> gears;
  public List<ParticleSystem> sparks;

  // private fields
  private Animator roomDoorAnimator;

  private void Start() {
    roomDoorAnimator = GetComponent<Animator>();
  }

  private void Update() {
    bool solved = true;
    for (int i = 0; i < gears.Count; i++) {
      solved &= gears[i].GetConnection();
    }
    if (solved) {
      for (int i=0; i<sparks.Count; i++){
        sparks[i].Play();
      }
      SoundManager.Play(SoundType.ZAP);
      gears.ForEach(gear => gear.ChangeState(ConductorState.DISABLED));
      roomDoorAnimator.SetTrigger("Door Open");
      SoundManager.Play(SoundType.DOOR);
    }
  }
}