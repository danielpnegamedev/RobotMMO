using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatMovementHandler : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void Set_Destination(Vector3 destination, float stoppingdistance = 0)
    {
        navMeshAgent.stoppingDistance = stoppingdistance;
        navMeshAgent.SetDestination(destination);
    }
}
