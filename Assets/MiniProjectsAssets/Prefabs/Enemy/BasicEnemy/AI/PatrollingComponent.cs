using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    [SerializeField] GameObject[] PatrollingPoint;
    private int _nextPatrollingCompIndex = 0;

    public GameObject GetNextPatrolPoint()
    {
        if (PatrollingPoint.Length > _nextPatrollingCompIndex)
        {
            GameObject patrolPoint = PatrollingPoint[_nextPatrollingCompIndex];
            _nextPatrollingCompIndex = (_nextPatrollingCompIndex + 1) % PatrollingPoint.Length;
            return patrolPoint;
        }

        return null;
    }
}
