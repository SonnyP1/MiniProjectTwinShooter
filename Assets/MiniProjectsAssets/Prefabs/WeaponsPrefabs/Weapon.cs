using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float TimeBetweenBulletsFirstAttack;
    [SerializeField] float TimeBetweenBulletsSecoundAttack;
    Projectile projectileType;
    Projectile secondaryProjectileType;
    List<Transform> spawnLocList = new List<Transform>();
    Coroutine waitForNextShot;
    float TimeToUnParent = .2f;
    public void SetProjectile(Projectile newProjectile) { projectileType = newProjectile; }
    public void SetSecoundaryProjectile(Projectile newProjectile) { secondaryProjectileType = newProjectile; }
    public void SetSpawnLoc(Transform newLoc)
    {
        spawnLocList.Add(newLoc);
    }
    public virtual void Attack()
    {
        FireBullet(projectileType, TimeBetweenBulletsFirstAttack);
    }

    public virtual void SecoundaryAttack()
    {
        FireBullet(secondaryProjectileType, TimeBetweenBulletsSecoundAttack);
    }

    void FireBullet(Projectile projectileType,float TimeBetweenBullets)
    {
        if(waitForNextShot==null)
        {
            waitForNextShot = StartCoroutine(WaitForNextShot(TimeBetweenBullets));
            //worry about this in a little bit
            foreach (Transform var in spawnLocList)
            {
                StartCoroutine(WaitToUnParentBullet(Instantiate(projectileType, var), TimeToUnParent));
            }
            
        }
    }
    public virtual void Reload()
    {

    }

    IEnumerator WaitToUnParentBullet(Projectile projectileToWait,float timeToUnParent)
    {
        yield return new WaitForSeconds(0.2f);
        if(projectileToWait)
            projectileToWait.transform.parent = null;
    }

    IEnumerator WaitForNextShot(float MaxTime)
    {
        float startTime = 0f;
        while(startTime < MaxTime)
        {
            startTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        waitForNextShot = null;
    }
}
