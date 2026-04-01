using UnityEngine;

public enum TumbleDir {
  UP,
  DOWN
}

public class Tumble : MonoBehaviour {
  public WireBoxTumbler tumbler;
  public TumbleDir tumbleDir;

  public void Grab() {
    if (tumbleDir == TumbleDir.UP) {
      tumbler.ChangeState(TumblerStates.TUMBLING_UP);
    }
    else {
      tumbler.ChangeState(TumblerStates.TUMBLING_DOWN);
    }
  }
}
