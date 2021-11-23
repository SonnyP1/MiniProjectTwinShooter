using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletsTest : MonoBehaviour
{
    [SerializeField] GameObject projectileToSpawn;
    [SerializeField] Transform spawnLoc;
    [SerializeField] float fireCooldown;
    void Start()
    {
        StartCoroutine(SpawnProjectileTimer());
    }

    private void SpawnProjectile()
    {
        //Debug.Log("SPAWN PROJECTILE");
        Instantiate(projectileToSpawn, spawnLoc);
    }
    
    IEnumerator SpawnProjectileTimer()
    {
        float startTime = 0;
        while (startTime < fireCooldown)
        {
            startTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SpawnProjectile();
        StopAllCoroutines();
        StartCoroutine(SpawnProjectileTimer());
    }
}
