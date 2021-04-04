using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IFSMState
{
    // agent's primary goal is to reduce the distance between itself and the player
    public FSMStateType StateName { get { return FSMStateType.Chase; } }
    public float MovementSpeed = 2.5f;
    public float Acceleration = 3.0f;
    public float AngularSpeed = 720.0f;
    public float POV = 60.0f;
    public string AnimationRunParamName = "Run";
    private readonly float MinChaseDistance = 2.0f;
    private NavMeshAgent ThisAgent;
    private SightLine SightLine;
    private float InitialPOV = 0.0f;
    private Animator ThisAnimator;

    private void Awake()
    {
        ThisAgent = GetComponent<NavMeshAgent>();
        SightLine = GetComponent<SightLine>();
        ThisAnimator = GetComponent<Animator>();
    }
    public void DoAction()
    {
        ThisAgent.SetDestination(SightLine.LastKnownSighting);
    }

    public void OnEnter()
    {
        InitialPOV = SightLine.FieldOfView;
        SightLine.FieldOfView = POV;
        ThisAgent.isStopped = false;
        ThisAgent.speed = MovementSpeed;
        ThisAgent.acceleration = Acceleration;
        ThisAgent.angularSpeed = AngularSpeed;
        ThisAnimator.SetBool(AnimationRunParamName, true);
    }

    public void OnExit()
    {
        SightLine.FieldOfView = InitialPOV;
        ThisAgent.isStopped = true;
    }

    public FSMStateType ShouldTransitionToState()
    {
        if (ThisAgent.remainingDistance <= MinChaseDistance)
        {
            return FSMStateType.Attack;
        }
        else if (!SightLine.IsTargetInSightLine)
        {
            return FSMStateType.Patrol;
        }
        return FSMStateType.Chase;
    }
}
