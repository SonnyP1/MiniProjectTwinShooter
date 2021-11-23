using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDamageTaken(int newAmt, int oldAmt, object attacker);
public delegate void OnHitPointDepleted();
public class HealthComp : MonoBehaviour
{
    [SerializeField] private int hitPoints;
    [SerializeField]  int maxHitPoints = 10;
    private object _attacker;
    public OnDamageTaken onDamageTaken;
    public OnHitPointDepleted onHitPointDepleted;

    public int GetMaxHitPoints() { return maxHitPoints;}
    

    public void CallTakeDmg(int amt)
    {
        TakeDmg(amt);
    }
    private void TakeDmg(int amt)
    {
        int oldVal = hitPoints;
        hitPoints -= amt;
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            if (onHitPointDepleted != null)
            {
                onHitPointDepleted.Invoke();
            }
        }
        else
        {
            //notify dmg taken
            if (oldVal != hitPoints)
            {
                if (onDamageTaken != null)
                {
                    onDamageTaken.Invoke(hitPoints, oldVal,_attacker);
                }
            }
        }
    }
}
