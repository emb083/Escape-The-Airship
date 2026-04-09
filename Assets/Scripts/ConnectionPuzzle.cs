using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionPuzzle : MonoBehaviour {
  // set in inspector
  public List<Gear> gears;

  // private fields
  private Animator roomDoorAnimator;
  private int[] code;

  private void Start() {
    roomDoorAnimator = GetComponent<Animator>();
    code = new[] { 3, 4, 6, 1 };
  }

  private void Update() {
    bool solved = true;
    for (int i = 0; i < gears.Count; i++) {
      solved &= gears[i].GetConnection();
    }
    if (solved) {
      gears.ForEach(gear => gear.ChangeState(GearState.DISABLED));
      roomDoorAnimator.SetTrigger("Door Open");
    }
  }
}