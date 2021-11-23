using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField]  int _dmg = 1;
    private GameObject _instigator;
    private Rigidbody projectileRB;
    private MeshRenderer meshRender;

    public void SetInstigator(GameObject newInstigator)
    {
        _instigator = newInstigator;
    }
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

        if (other.gameObject == _instigator)
        {
            return;
        }
        if(other.gameObject.GetComponent<Projectile>() || other.GetComponent<InteractComp>() || other.GetComponent<Weapon>())
        {
            return;
        }

        
        //if has health comp do dmg then destroy
        BoxCollider hitBox = other.GetComponent<BoxCollider>();
        if (hitBox)
        {
            HealthComp hitBoxHealthComp = hitBox.gameObject.GetComponent<HealthComp>();
            if (hitBoxHealthComp)
            {
                hitBoxHealthComp.CallTakeDmg(_dmg);
            }
        }

        Destroy(gameObject);
    }
}
