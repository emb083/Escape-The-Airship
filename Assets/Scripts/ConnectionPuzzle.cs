using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionPuzzle : MonoBehaviour {
  // set in inspector
  public List<Conductor> gears;

  // private fields
  private Animator roomDoorAnimator;
  private int[] code;

  private void Start() {
    roomDoorAnimator = GetComponent<Animator>();
  }

  private void Update() {
    bool solved = true;
    for (int i = 0; i < gears.Count; i++) {
      solved &= gears[i].GetConnection();
    }
    if (solved) {
      SoundManager.Play(SoundType.ZAP);
      gears.ForEach(gear => gear.ChangeState(ConductorState.DISABLED));
      roomDoorAnimator.SetTrigger("Door Open");
      SoundManager.Play(SoundType.DOOR);
    }
  }
}