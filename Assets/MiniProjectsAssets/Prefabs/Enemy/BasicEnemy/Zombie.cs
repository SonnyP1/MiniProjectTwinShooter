using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private HealthComp _healthComp;
    //private Animator _animator;
    private AIController _aiController;
    private int _layerUpperBodyID;
    
    [SerializeField] GameObject projectileToSpawn;
    [SerializeField] Transform[] spawnLoc;
    [SerializeField] float cooldownShoot;
    private float _startTime = 0;

    private void Start()
    {
        _healthComp = GetComponent<HealthComp>();
        //_animator = GetComponent<Animator>();
        _aiController = GetComponent<AIController>();
        //_layerUpperBodyID = _animator.GetLayerIndex("UpperBody");
        if (_healthComp)
        {
            _healthComp.onDamageTaken += TookDamage;
            _healthComp.onHitPointDepleted += Death;
        }

        if (_aiController)
        {
            _aiController.onAttack += Attack;
        }
    }

    private void Attack()
    {
        Debug.Log("SHOOT");

        while (_startTime < cooldownShoot)
        {
            _startTime += Time.deltaTime;
            return;
        }

        _startTime = 0;
        SpawnProjectile();
    }
    private void SpawnProjectile()
    {
        //Debug.Log("SPAWN PROJECTILE");
        for (int i = 0; i < spawnLoc.Length; i++)
        {
            Instantiate(projectileToSpawn, spawnLoc[i]);
        }
    }
    private void Death()
    {
        //play death animation
        Debug.Log("AM DEAD RIP");
        //_animator.SetTrigger("DeathTrigger");
    }
    
    private void TookDamage(int newamt, int oldamt,object attacker)
    {
        Debug.Log($"I took: {oldamt-newamt} of damage");
        _aiController.SetBlackBoardKey("Target",((GameObject)attacker).transform.position);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    public void SetLayerWeightToZero()
    {
        //_animator.SetLayerWeight(_layerUpperBodyID,0);
    }
    


}
