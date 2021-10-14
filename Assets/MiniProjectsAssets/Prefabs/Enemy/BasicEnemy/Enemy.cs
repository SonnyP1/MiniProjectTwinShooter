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
    [SerializeField] private float distanceOfStrafeRange;
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
        navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        FindPlayer();
        if (currentTarget != null)
        {
            transform.LookAt(currentTarget.transform);
            float distanceBetweenPlayerAndEnemy =
                Vector3.Distance(currentTarget.transform.position, transform.position);
            if (distanceOfShootingRange < distanceBetweenPlayerAndEnemy)
            {
                navMeshAgent.SetDestination(currentTarget.transform.position);
            }
            else
            {
                //Shoot
                
                //Strafe
                CalculateWhereToStrafe(distanceBetweenPlayerAndEnemy);
            }
        }
        
    }

    void CalculateWhereToStrafe(float distanceBetween)
    {
        if (distanceOfStrafeRange > distanceBetween)
        {
            Vector3 offsetPos = transform.position - currentTarget.transform.position;

            Vector3 dir = Vector3.Cross(offsetPos, PickRandomDir());
            navMeshAgent.SetDestination(offsetPos + dir);
        }
        
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
        
        return PickRandomDir();
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
