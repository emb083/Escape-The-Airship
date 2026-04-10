using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionPuzzle : MonoBehaviour {
  // set in inspector
  public List<Conductor> gears;
  public List<ParticleSystem> sparks;
  public GameObject keyGear;
  public Animator gearFalling;

  // private fields
  private Animator roomDoorAnimator;
  private bool isSolved= false;
  private void Start() {
    keyGear.SetActive(false);
    roomDoorAnimator = GetComponent<Animator>();
  }

  private void Update() {
    if (isSolved)
    {
      return;
    }
    bool solved=true;
    for (int i = 0; i < gears.Count; i++) {
      solved &= gears[i].GetConnection();
    }
    if (solved) {
      isSolved=true;
      for (int i=0; i<sparks.Count; i++){
        sparks[i].Play();
      }
      SoundManager.Play(SoundType.ZAP);
      gears.ForEach(gear => gear.ChangeState(ConductorState.DISABLED));
      roomDoorAnimator.SetTrigger("Door Open");
      keyGear.SetActive(true);
      gearFalling.SetTrigger("falling");
      SoundManager.Play(SoundType.FALL);
      SoundManager.Play(SoundType.DOOR);
    }
  }
}