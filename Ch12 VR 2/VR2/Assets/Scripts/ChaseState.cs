using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IFSMState
{
    public FSMStateType StateName { get { return FSMStateType.Chase; } }
    public float MinChaseDistance = 2.0f;

    private Transform Player = null;
    private NavMeshAgent ThisAgent = null;

    void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ThisAgent = GetComponent<NavMeshAgent>();
        if (Player == null)
            Debug.Log("Waking up, can't find player");
        if (ThisAgent == null)
            Debug.Log("Waking up, can't find my own NavMeshAgent");
    }

    public void OnEnter()
    {
        // We are now moving toward the player
        ThisAgent.isStopped = false;
    }

    public void OnExit()
    {
        // Suppress further movement
        ThisAgent.isStopped = true;
    }

    public void DoAction()
    {
        // move toward player
        ThisAgent.SetDestination(Player.position);
    }

    public FSMStateType ShouldTransitionToState()
    {
        // If we get close enough, enter the Attack state. Otherwise, keep Chasing
        float DistanceToDest = Vector3.Distance(transform.position, Player.position);
        if (DistanceToDest <= MinChaseDistance)
        {
            return FSMStateType.Attack;
        }
        return FSMStateType.Chase;
    }
}
