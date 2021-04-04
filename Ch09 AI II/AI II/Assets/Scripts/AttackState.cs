using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class AttackState : MonoBehaviour,IFSMState
{
    public FSMStateType StateName { get { return FSMStateType.Attack; } }
    public string AnimationAttackParamName = "Attack";
    public float EscapeDistance = 10.0f;
    public float MaxAttackDistance = 2.0f;
    public string TargetTag = "Player";
    public float DelayBetweenAttacks = 2.0f;

    private Animator ThisAnimator;
    private NavMeshAgent ThisAgent;
    private bool IsAttacking = false;
    private Transform Target;

    private void Awake()
    {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
    }
    public void DoAction()
    {
        IsAttacking = Vector3.Distance(Target.position, transform.position) < MaxAttackDistance;

        if (!IsAttacking)
        {
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(Target.position);
        }
    }

    public void OnEnter()
    {
        // A coroutine is a function that can break at a specified point, and then execution will continue
        // from that point during the next frame. Typically used to build complex behavior that can run over
        // several frames without slowing down gameplay by trying to do too much during a single frame
        StartCoroutine(DoAttack());
    }

    public void OnExit()
    {
        ThisAgent.isStopped = true;
        IsAttacking = false;
        StopCoroutine(DoAttack());
    }


    public FSMStateType ShouldTransitionToState()
    {
        if (Vector3.Distance(Target.position, transform.position) > EscapeDistance)
        {
            return FSMStateType.Chase;
        }
        return FSMStateType.Attack;
    }

    private IEnumerator DoAttack()
    {
        // returns an IEnumerator which is passed to StartCoroutine and StopCoroutine in the OnEnter and OnExit functions
        // This routine runs on a frame-safe infinite loop  for as long as the FSM is in the attack state
        while(true)
        {
            if (IsAttacking)
            {
                Debug.Log("Attacking Player");
                ThisAnimator.SetTrigger(AnimationAttackParamName);
                ThisAgent.isStopped = true;
                // Run the attack animation, stop the chick and wait
                yield return new WaitForSeconds(DelayBetweenAttacks);
            }
            yield return null;
        }
    }
}
