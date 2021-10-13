using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Range
    [SerializeField] private float radiusOfFindRange;
    [SerializeField] private float distanceOfShootingRange;
    //Movement
    [SerializeField] private float enemyMovementSpeed;
    [SerializeField] private LayerMask maskToLookFor;
    private NavMeshAgent navMeshAgent;
    private GameObject currentTarget;
    
    //Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radiusOfFindRange);
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindPlayer();
        if (currentTarget != null)
        {
            if (distanceOfShootingRange < Vector3.Distance(currentTarget.transform.position, transform.position))
            {
                navMeshAgent.SetDestination(currentTarget.transform.position);
                //navMeshAgent.velocity = (Vector3.forward * Time.deltaTime)*enemyMovementSpeed;
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                //start shooting and strafe
            }
        }
        
    }
    void FindPlayer()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radiusOfFindRange,maskToLookFor);
        if (cols.Length != 0)
        {
            foreach (Collider col in cols)
            {
                if (col.GetComponent<PlayerScript>())
                {
                    currentTarget = col.gameObject;
                    return;
                }
            }
        }
        currentTarget = null;
    }
}
