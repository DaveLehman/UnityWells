using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public FSMStateType StartState = FSMStateType.Patrol;
    private IFSMState[] StatePool;
    private IFSMState CurrentState;
    private readonly IFSMState EmptyAction = new EmptyAction();

    void Awake()
    {
        // retrieves all scripts attached to the object that implement IFSMState
        StatePool = GetComponents<IFSMState>();
    }
    void Start()
    {
        CurrentState = EmptyAction;
        TransistionToState(StartState);
    }

    private void TransistionToState(FSMStateType StateName)
    {
        // end the current state
        CurrentState.OnExit();
        CurrentState = GetState(StateName);
        CurrentState.OnEnter();
        Debug.Log("Transitioned to " + CurrentState.StateName);
    }

    IFSMState GetState(FSMStateType StateName)
    {
        foreach (var state in StatePool)
        {
            if (state.StateName == StateName)
            {
                return state;
            }
        }
        return EmptyAction;
    }
    void Update()
    {
        CurrentState.DoAction();

        FSMStateType TransitionState = CurrentState.ShouldTransitionToState();
        if (TransitionState != CurrentState.StateName)
        {
            Debug.Log("FSM.Update is transitioning states");
            TransistionToState(TransitionState);
        }
    }
}
