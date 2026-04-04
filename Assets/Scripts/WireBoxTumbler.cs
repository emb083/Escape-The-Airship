using System;
using System.Collections.Generic;
using UnityEngine;
using State = TumblerStates;

public enum TumblerStates {
  IDLE,
  TUMBLING_UP,
  TUMBLING_DOWN,
  DISABLED
}

public class WireBoxTumbler : MonoBehaviour {
    public int numberOfSides=10;
  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;

  private Dictionary<State, Action> stateEnterMethods;
  private Dictionary<State, Action> stateStayMethods;
  private Dictionary<State, Action> stateExitMethods;
  private int number;
  private Quaternion curRotation;
  private Quaternion targetRotation;
  private float t; // keeps track of information on starting and stopping.
  
  private void Start() {
    State = State.IDLE;
    number=0;
    
    allowedTransitions = new() {
      new(State.IDLE, State.TUMBLING_UP),
      new(State.IDLE, State.TUMBLING_DOWN),
      new(State.TUMBLING_UP, State.IDLE),
      new(State.TUMBLING_DOWN, State.IDLE),
      new(State.IDLE, State.DISABLED),

    };

    stateEnterMethods = new() {
      [State.IDLE] = StateEnter_Idle,
      [State.TUMBLING_UP] = StateEnter_TumblingUp,
      [State.TUMBLING_DOWN] = StateEnter_TumblingDown,
      [State.DISABLED]= StateEnter_Disabled,
    };
    stateStayMethods = new() {
      [State.IDLE] = StateStay_Idle,
      [State.TUMBLING_UP] = StateStay_TumblingUp,
      [State.TUMBLING_DOWN] = StateStay_TumblingDown,
      [State.DISABLED]= StateStay_Disabled,
    };
    stateExitMethods = new() {
      [State.IDLE] = StateExit_Idle,
      [State.TUMBLING_UP] = StateExit_TumblingUp,
      [State.TUMBLING_DOWN] = StateExit_TumblingDown,
      [State.DISABLED]= StateExit_Disabled,
    };
  }

  private void Update() {
    if (stateStayMethods.ContainsKey(State)) {
      stateStayMethods[State].Invoke();
    }
  }
  public int GetNumber(){ // may not need this function.
        return number;
  }

  public void ChangeState(State newState) {
    if (allowedTransitions.Contains(new(State, newState))) {
      stateExitMethods[State].Invoke();
      State = newState;
      stateEnterMethods[State].Invoke();
    }
  }
  // ENTER
  private void StateEnter_Idle() {
    // idle enter method
  }
  private void StateEnter_TumblingUp() {
    // using enter method
    curRotation=transform.rotation;
    targetRotation=curRotation * Quaternion.Euler(Vector3.down * (360/ numberOfSides));
    t=0;
    // SoundManager.Play(SoundType.TUMBLE); 
  }
  private void StateEnter_TumblingDown() {
    // used enter method
    curRotation=transform.rotation;
    targetRotation=curRotation * Quaternion.Euler(Vector3.up * (360/ numberOfSides));
    t=0;
    // SoundManager.Play(SoundType.TUMBLE); 
  }

  private void StateEnter_Disabled(){
     
  }
  // STAY
private void StateStay_Idle() {
    // idle enter method
  }
  private void StateStay_TumblingUp() {
    // using enter method
    t += 1*Time.deltaTime;
    transform.rotation=Quaternion.Lerp(curRotation,targetRotation,t);
    if (t >= 1)
        {
            transform.rotation=targetRotation;
            ChangeState(State.IDLE);
        }
  }
  private void StateStay_TumblingDown() {
    // used enter method
    t += 1*Time.deltaTime;
    transform.rotation=Quaternion.Lerp(curRotation,targetRotation,t);
    if (t >= 1)
        {
            transform.rotation=targetRotation;
            ChangeState(State.IDLE);
        }
  }

  private void StateStay_Disabled(){
     
  }
// EXIT
  private void StateExit_Idle() {
    // idle enter method
  }
  private void StateExit_TumblingUp() {
    // using enter method
    number++;
    number %= numberOfSides;
  }
  private void StateExit_TumblingDown() {
    // used enter method
    number--;
    if (number < 0)
        {
            number += numberOfSides;
        }
  }

  private void StateExit_Disabled(){
     
  }
}
