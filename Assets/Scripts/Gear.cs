using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using State = GearState;

public enum GearState {
  IDLE,
  TURNING,
  DISABLED
}

public class Gear : MonoBehaviour {
  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;

  private Dictionary<State, Action> stateEnterMethods;
  private Dictionary<State, Action> stateStayMethods;
  private Dictionary<State, Action> stateExitMethods;

  private Quaternion curRot;
  private Quaternion targetRot;
  private float t;
  private bool connected;

  private Coroutine turningCoroutine;

  public int gearID;

  private void Start() {
    State = State.IDLE;
    turningCoroutine = null;

    connected = (transform.rotation.x == 0) ? true : false;
    allowedTransitions = new() {
      new(State.IDLE, State.TURNING),
      new(State.TURNING, State.IDLE),
      new(State.IDLE, State.DISABLED),
    };

    stateEnterMethods = new() {
      [State.IDLE] = StateEnter_Idle,
      [State.TURNING] = StateEnter_Turning,
      [State.DISABLED] = StateEnter_Disabled,
    };
    stateStayMethods = new() {
      [State.IDLE] = StateStay_Idle,
      [State.TURNING] = StateStay_Turning,
      [State.DISABLED] = StateStay_Disabled,
    };
    stateExitMethods = new() {
      [State.IDLE] = StateExit_Idle,
      [State.TURNING] = StateExit_Turning,
      [State.DISABLED] = StateExit_Disabled,
    };
  }

  private void Update() {
    if (stateStayMethods.ContainsKey(State)) {
      stateStayMethods[State].Invoke();
    }
  }

  private IEnumerator Turn() {
    while (t < 1) {
      t += 1 * Time.deltaTime;
      transform.rotation = Quaternion.Lerp(curRot, targetRot, t);
      yield return new WaitForEndOfFrame();
    }
    if (targetRot.x < 360){
        targetRot.x = targetRot.x % 360;
    }
    transform.rotation = targetRot;
    ChangeState(State.IDLE);
  }

  public bool GetConnection() => connected;

  public void ChangeState(State newState) {
    if (allowedTransitions.Contains(new(State, newState))) {
      stateExitMethods[State].Invoke();
      State = newState;
      stateEnterMethods[State].Invoke();
    }
  }

  private void StateEnter_Idle() {
    // idle enter method
  }

  private void StateEnter_Turning() {
    curRot = transform.rotation;
    targetRot = curRot * Quaternion.Euler(Vector3.right * 90);
    t = 0;
    //SoundManager.Play(SoundType.TUMBLE);
    turningCoroutine = StartCoroutine(Turn());
  }

  private void StateEnter_Disabled() {
    // used enter method
  }

  private void StateStay_Idle() {
    // idle enter method
  }

  private void StateStay_Turning() {
  }

  private void StateStay_Disabled() {
    // used enter method
  }

  private void StateExit_Idle() {
    // idle enter method
  }

  private void StateExit_Turning() {
    float curRot = transform.localEulerAngles.x;
    if (curRot == 0 || curRot == -180){
        connected = true;
    } else {
        connected = false;
    }
  }

  private void StateExit_Disabled() {
    // used enter method
  }
}