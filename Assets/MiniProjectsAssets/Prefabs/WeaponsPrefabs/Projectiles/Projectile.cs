using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileInstigator
{
    Player,
    Enemy
}

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] ProjectileInstigator _instigator;
    private Rigidbody projectileRB;
    private MeshRenderer meshRender;
    
    void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
        meshRender = GetComponentInChildren<MeshRenderer>();
        StartCoroutine(WaitToCheckIfInScreenSpace());
    }

    // Update is called once per frame
    void Update()
    {
        projectileRB.AddForce(transform.forward* projectileSpeed);
    }

    IEnumerator WaitToCheckIfInScreenSpace()
    {
        yield return new WaitForSeconds(.1f);
        while(meshRender.isVisible)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _instigator == ProjectileInstigator.Player)
        {
            return;
        }

        if (other.CompareTag("Enemy") && _instigator == ProjectileInstigator.Enemy)
        {
            return;
        }
        
        if(other.gameObject.GetComponent<Projectile>() || other.GetComponent<InteractComp>() || other.GetComponent<Weapon>())
        {
            return;
        }
        
        //if has health comp do dmg then destroy
        Destroy(gameObject);
    }
}
