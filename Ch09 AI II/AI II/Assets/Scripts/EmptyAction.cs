using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyAction : IFSMState
{
    // This class is an empty state -- removes the need for FSM comtroller to check for a null state. If a required state can't be found we'll return this empty state
    public FSMStateType StateName { get { return FSMStateType.None; } }

    public void DoAction()
    {
        
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public FSMStateType ShouldTransitionToState()
    {
        return FSMStateType.None;
    }
}
