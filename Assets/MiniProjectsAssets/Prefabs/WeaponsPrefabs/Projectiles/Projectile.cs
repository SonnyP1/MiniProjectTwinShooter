using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody projectileRB;
    [SerializeField] float projectileSpeed;
    MeshRenderer meshRender;
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
        if(other.gameObject.GetComponent<Projectile>())
        {
            return;
        }
        Destroy(gameObject);
    }
}
