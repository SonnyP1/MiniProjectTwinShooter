using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float TimeBetweenBulletsFirstAttack;
    [SerializeField] float TimeBetweenBulletsSecoundAttack;
    [SerializeField] int CurrentAmmo;
    [SerializeField] int MaxAmmo;
    [SerializeField] float ReloadTime;
    [SerializeField] MeshRenderer meshRender;
    Projectile projectileType;
    Projectile secondaryProjectileType;
    List<Transform> spawnLocList = new List<Transform>();
    Coroutine waitForNextShot;
    Coroutine Reloading;
    float TimeToUnParent = .2f;
    private void Awake()
    {
        CurrentAmmo = MaxAmmo;
    }
    public void SetProjectile(Projectile newProjectile) { projectileType = newProjectile; }
    public void SetSecoundaryProjectile(Projectile newProjectile) { secondaryProjectileType = newProjectile; }
    public void SetSpawnLoc(Transform newLoc)
    {
        spawnLocList.Add(newLoc);
    }
    public virtual void Attack()
    {
        if (!isReloading() && CurrentAmmo > 0 && waitForNextShot == null)
        {
            FireBullet(projectileType, TimeBetweenBulletsFirstAttack);
            CurrentAmmo--;
        }
    }
    public virtual void SecoundaryAttack()
    {
        if (!isReloading() && CurrentAmmo > 0 && waitForNextShot == null)
        {
            FireBullet(secondaryProjectileType, TimeBetweenBulletsSecoundAttack);
            CurrentAmmo--;
        }
    }

    void FireBullet(Projectile projectileType,float TimeBetweenBullets)
    {
        if(waitForNextShot==null)
        {
            waitForNextShot = StartCoroutine(WaitForNextShot(TimeBetweenBullets));
            foreach (Transform var in spawnLocList)
            {
                StartCoroutine(WaitToUnParentBullet(Instantiate(projectileType, var), TimeToUnParent));
            }
        }
    }
    public virtual void Reload()
    {
        if (!isReloading())
        {
            Reloading = StartCoroutine(Reload(ReloadTime));
        }
    }
    IEnumerator Reload(float reloadTime)
    {
        float startTime = 0;
        while(startTime < ReloadTime)
        {
            startTime += Time.deltaTime;
            Debug.Log("RELOADING: " + startTime);
            yield return new WaitForEndOfFrame();
        }
        CurrentAmmo = MaxAmmo;
        Reloading = null;
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

    bool isReloading() { if (Reloading == null) { return false; } else { return true; } }
    public void SetGunVisibility(bool isVis)
    {
        if (meshRender != null)
        {
            meshRender.enabled = isVis;
        }
        else { Debug.Log("meshRender does not exist"); }
    }
}
