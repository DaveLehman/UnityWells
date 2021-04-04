using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSMState
{
    // Each state will have a corresponding class that implements this interface.
    FSMStateType StateName { get; }

    
    void OnEnter(/*Called at the beginning of a state transition*/);
    void OnExit(/*Called during a state transition as we exit the state and move to a new one*/);
    void DoAction(/*Where the state's action occurs*/);
    FSMStateType ShouldTransitionToState(/*Queried directly after after a call to DoAction. Will return a different FSMStateType to indicate that FSM should transition to a 
                                          * different state or return the current one */);
}
