using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IFSMState
{
    /* In this state, the enemy will rotate to face the player and attack by shooting projectiles using a particle system
     * */
    public FSMStateType StateName {  get { return FSMStateType.Attack; } }
    public ParticleSystem WeaponPS = null;
    private Transform ThisPlayer = null;

    void Awake()
    {
        ThisPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void OnEnter()
    {
        // called when we transition to this state -- do setup here
        WeaponPS.Play();
    }

    public void OnExit()
    {
        // do cleanup here
        WeaponPS.Stop();
    }

    public void DoAction()
    {
        // Called every frame. Rotates the enemy to point at the player
        Vector3 Dir = (ThisPlayer.position - transform.position).normalized;
        Dir.y = 0;
        transform.rotation = Quaternion.LookRotation(Dir, Vector3.up);
    }

    public FSMStateType ShouldTransitionToState()
    {
        // Called every frame. Once we are in the attack state, we don't want to change it do anything else
        return FSMStateType.Attack;
    }


}
