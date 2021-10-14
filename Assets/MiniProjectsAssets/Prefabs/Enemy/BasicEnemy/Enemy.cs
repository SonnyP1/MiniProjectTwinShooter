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
    [SerializeField] private float radiusOfForgetRange;
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
        Gizmos.DrawWireSphere(transform.position,radiusOfForgetRange);
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.speed = enemyMovementSpeed;
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            FindPlayer();
        }
        else
        {
            transform.LookAt(currentTarget.transform);
            CalculateWhereToMove();
            CheckIfPlayerOutOfRangeThenForgetPlayer();
        }
    }


    void CalculateWhereToMove()
    {
        if (currentTarget != null)
        {
            float distanceBetweenPlayerAndEnemy = Vector3.Distance(currentTarget.transform.position, transform.position);
            Debug.Log("This is being called");
            if (distanceBetweenPlayerAndEnemy > distanceOfShootingRange)
            {
                navMeshAgent.SetDestination(currentTarget.transform.position);
            }
            else
            {
                CalculateWhereToStrafe();
            }
        }
    }

    void CalculateWhereToStrafe()
    {
        Vector3 offsetPos = transform.position - currentTarget.transform.position;
        Vector3 dir = Vector3.Cross(offsetPos, PickRandomDir()); 
        navMeshAgent.SetDestination(offsetPos + dir);

    }
    Vector3 PickRandomDir()
    {
        int randomNum = UnityEngine.Random.Range(1, 4);
        switch (randomNum)
        {
            case 1:
                return Vector3.up;
            case 2:
                return Vector3.right;
            case 3:
                return Vector3.left;
        }

        return Vector3.zero;
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
                    CalculateWhereToMove();
                    return;
                }
            }
        }
    }


    void CheckIfPlayerOutOfRangeThenForgetPlayer()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radiusOfFindRange,maskToLookFor);
        if (cols.Length == 0)
        {
            currentTarget = null;
        }
    }
}
