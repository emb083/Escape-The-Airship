using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WireBoxPuzzle : MonoBehaviour
{
     // set in inspector
  public List<WireBoxTumbler> tumblers;

  // private fields
  private Animator lidDoorAnimator;
  private int[] code;

  private void Start() {
    lidDoorAnimator = GetComponent<Animator>();
    code = new[] { 3, 4, 6 };
  }

  private void Update() {
    bool comboCorrect = true;
    for (int i = 0; i < tumblers.Count; i++) {
      comboCorrect &= (tumblers[i].GetNumber() == code[i]);
    }
    if (comboCorrect) {
      //foreach (var tumbler in tumblers) {
      //  tumbler.ChangeState(TumblerStates.DISABLED);
      //}
      tumblers.ForEach(tumbler => tumbler.ChangeState(TumblerStates.DISABLED));
      lidDoorAnimator.SetTrigger("lidopen");
      // SoundManager.Play(SoundType.PICKUP); 
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
