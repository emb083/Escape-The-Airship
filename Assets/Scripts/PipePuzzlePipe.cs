using System;
using System.Collections.Generic;
using UnityEngine;
using State = PipeStates;
public enum PipeStates

{
    IDLE,
    ROTATE_CLW,
    ROTATE_CTRCLW,
    DISABLED
}

public class PipePuzzlePipe : MonoBehaviour {
    public int numberOfSides = 4;
    public State State { get; private set; }
    private HashSet<KeyValuePair<State, State>> allowedTransitions;

    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;
    private Dictionary<State, Action> stateExitMethods;
    private int number;
    private Quaternion curRotation;
    private Quaternion targetRotation;
    private float t; // keeps track of information on starting and stopping.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        State = State.IDLE;
        number = 0;

       allowedTransitions = new() {
        new(State.IDLE, State.ROTATE_CLW),
        new(State.IDLE, State.ROTATE_CTRCLW),
        new(State.ROTATE_CLW, State.IDLE),
        new(State.ROTATE_CTRCLW, State.IDLE),
        new(State.IDLE, State.DISABLED),

    };

        stateEnterMethods = new()
        {
            [State.IDLE] = StateEnter_Idle,
            [State.ROTATE_CLW] = StateEnter_RotateClock,
            [State.ROTATE_CTRCLW] = StateEnter_RotateCounter,
            [State.DISABLED] = StateEnter_Disabled,
        };
        stateStayMethods = new()
        {
            [State.IDLE] = StateStay_Idle,
            [State.ROTATE_CLW] = StateStay_RotateClock,
            [State.ROTATE_CTRCLW] = StateStay_RotateCounter,
            [State.DISABLED] = StateStay_Disabled,
        };
        stateExitMethods = new()
        {
            [State.IDLE] = StateExit_Idle,
            [State.ROTATE_CLW] = StateExit_RotateClock,
            [State.ROTATE_CTRCLW] = StateExit_RotateCounter,
            [State.DISABLED] = StateExit_Disabled,
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (stateStayMethods.ContainsKey(State)){
            stateStayMethods[State].Invoke();
        }
    }

    public int GetNumber()
    { // may not need this function.
        return number;
    }

    public void ChangeState(State newState)
    {
        if (allowedTransitions.Contains(new(State, newState)))
        {
            stateExitMethods[State].Invoke();
            State = newState;
            stateEnterMethods[State].Invoke();
        }
    }
    // ENTER
    private void StateEnter_Idle()
    {
        // idle enter method
    }
    private void StateEnter_RotateClock()
    {
        // using enter method
        curRotation = transform.rotation;
        targetRotation = curRotation * Quaternion.Euler(Vector3.right * (360 / numberOfSides));
        t = 0;
        SoundManager.Play(SoundType.PIPE);
    }
    private void StateEnter_RotateCounter()
    {
        // used enter method
        curRotation = transform.rotation;
        targetRotation = curRotation * Quaternion.Euler(Vector3.left * (360 / numberOfSides));
        t = 0;
        SoundManager.Play(SoundType.PIPE);
    }

    private void StateEnter_Disabled()
    {

    }
    // STAY
    private void StateStay_Idle()
    {
        // idle enter method
    }
    private void StateStay_RotateClock()
    {
        // using enter method
        t += 1 * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(curRotation, targetRotation, t);
        if (t >= 1)
        {
            transform.rotation = targetRotation;
            ChangeState(State.IDLE);
        }
    }
    private void StateStay_RotateCounter()
    {
        // used enter method
        t += 1 * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(curRotation, targetRotation, t);
        if (t >= 1)
        {
            transform.rotation = targetRotation;
            ChangeState(State.IDLE);
        }
    }

    private void StateStay_Disabled()
    {

    }
    // EXIT
    private void StateExit_Idle()
    {
        // idle enter method
    }
    private void StateExit_RotateClock()
    {
        // using enter method
        number++;
        number %= numberOfSides;
    }
    private void StateExit_RotateCounter()
    {
        // used enter method
        number--;
        if (number < 0)
        {
            number += numberOfSides;
        }
    }

    private void StateExit_Disabled()
    {

    }
}