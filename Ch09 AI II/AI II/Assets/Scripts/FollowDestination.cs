using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class FollowDestination : MonoBehaviour
{
    public Transform Destination = null;
    private NavMeshAgent ThisAgent = null;


    void Awake()
    {
        ThisAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // this is a demo -- for real, should check the if the destination has moved before calling SetDestination to reduce the number of calculations
        ThisAgent.SetDestination(Destination.position);
    }
}
