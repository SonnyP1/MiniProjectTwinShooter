using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private HealthComp _healthComp;
    private Animator _animator;
    private AIController _aiController;
    private int _layerUpperBodyID;
    private NavMeshAgent _navMeshAgent;
    private BoxCollider _boxCollider;
    
    [SerializeField] GameObject projectileToSpawn;
    [SerializeField] Transform[] spawnLoc;
    [SerializeField] float cooldownShoot;
    private float _startTime = 0;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthComp = GetComponent<HealthComp>();
        _animator = GetComponent<Animator>();
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

    private void Update()
    {
        if (_startTime < cooldownShoot)
        {
            _startTime += Time.deltaTime;
        }
    }

    private void Attack()
    {
        Debug.Log("SHOOT");
        if (_startTime >= cooldownShoot)
        {
            _startTime = 0;
            SpawnProjectile();
        }
        
    }
    private void SpawnProjectile()
    {
        //Debug.Log("SPAWN PROJECTILE");
        for (int i = 0; i < spawnLoc.Length; i++)
        {
            GameObject newProjectile = Instantiate(projectileToSpawn, spawnLoc[i]);
            newProjectile.GetComponent<Projectile>().SetInstigator(gameObject);
            StartCoroutine(WaitToUnParentBullet(newProjectile.GetComponent<Projectile>(), 0.5f));
        }
    }
    private void Death()
    {
        _animator.SetTrigger("Death");
        Debug.Log("AM DEAD RIP");
        Destroy(_aiController);
        Destroy(_navMeshAgent);
        Destroy(_boxCollider);
        StartCoroutine(DestroyGameobject());
    }
    
    private void TookDamage(int newamt, int oldamt,object attacker)
    {
        Debug.Log($"I took: {oldamt-newamt} of damage");
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    public void SetLayerWeightToZero()
    {
        //_animator.SetLayerWeight(_layerUpperBodyID,0);
    }
    
    IEnumerator WaitToUnParentBullet(Projectile projectileToWait,float timeToUnParent)
    {
        yield return new WaitForSeconds(timeToUnParent);
        if(projectileToWait)
            projectileToWait.transform.parent = null;
    }

    IEnumerator DestroyGameobject()
    {
        yield return new WaitForSeconds(5);
        DestroySelf();
    }

}
