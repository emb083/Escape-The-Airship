using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WireBoxPuzzle : MonoBehaviour
{
     // set in inspector
  public List<WireBoxTumbler> tumblers;

  // private fields
  public Animator lidDoorAnimator;
  private int[] code;
  private bool isOpen=false;

  private void Start() {
    //lidDoorAnimator = GetComponent<Animator>();
    code = new[] { 3, 4, 6 };
  }

  private void Update() {
   if (isOpen) // avoids the sound from being spammed since it is in update.
    {
      return;
    }
    bool comboCorrect = true;
    for (int i = 0; i < tumblers.Count; i++) {
      comboCorrect &= (tumblers[i].GetNumber() == code[i]);
    }
    if (comboCorrect) {
      //foreach (var tumbler in tumblers) {
      //  tumbler.ChangeState(TumblerStates.DISABLED);
      //}
      isOpen=true; // if open is true the sound will be played.
      SoundManager.Play(SoundType.OPEN);
      tumblers.ForEach(tumbler => tumbler.ChangeState(TumblerStates.DISABLED));
      lidDoorAnimator.SetTrigger("lidopen");
       
    }

    //bool[] correctNumbers = new bool[] {
    //  (tumblers[0].GetNumber() == code[0]),
    //  (tumblers[1].GetNumber() == code[1]),
    //  (tumblers[2].GetNumber() == code[2]),
    //  (tumblers[3].GetNumber() == code[3])
    //};
    //bool comboCorrect = correctNumbers.All();
  }
}
