using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        Shotgun,
        Pistol
    }
    [SerializeField] int regularAmmoReduce;
    [SerializeField] int altAmmoReduce;
    [SerializeField] float timeBetweenBulletsFirstAttack;
    [SerializeField] float timeBetweenBulletsSecoundAttack; 
    [SerializeField] int currentAmmo;
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] MeshRenderer meshRender;
    [SerializeField] WeaponType typeOfWeapon;
    private Projectile projectileType;
    private Projectile secondaryProjectileType;
    private List<Transform> spawnLocList = new List<Transform>();
    private Coroutine waitForNextShot;
    private Coroutine reloadingCoroutine;
    private float timeToUnParent = .2f;

    public WeaponType GetWeaponType() { return typeOfWeapon;}

    public Coroutine GetReloadingCoroutine() { return reloadingCoroutine;}
    public int GetMaxAmmo() { return maxAmmo;}

    public int GetCurrentAmmo() { return currentAmmo;}
    private void Awake()
    {
        currentAmmo = maxAmmo;
    }
    public void SetProjectile(Projectile newProjectile) { projectileType = newProjectile; }
    public void SetSecoundaryProjectile(Projectile newProjectile) { secondaryProjectileType = newProjectile; }
    public void SetSpawnLoc(Transform newLoc)
    {
        spawnLocList.Add(newLoc);
    }
    public virtual void Attack()
    {
        if (!IsReloading() && currentAmmo > 0 && waitForNextShot == null)
        {
            FireBullet(projectileType, timeBetweenBulletsFirstAttack);
            currentAmmo = Mathf.Clamp(currentAmmo - regularAmmoReduce, 0, maxAmmo);
        }
    }
    public virtual void SecondaryAttack()
    {
        if (!IsReloading() && currentAmmo > 0 && waitForNextShot == null)
        {
            FireBullet(secondaryProjectileType, timeBetweenBulletsSecoundAttack);
            currentAmmo = Mathf.Clamp(currentAmmo - altAmmoReduce,0,maxAmmo);
        }
    }

    void FireBullet(Projectile projectileType,float timeBetweenBullets)
    {
        if(waitForNextShot==null)
        {
            waitForNextShot = StartCoroutine(WaitForNextShot(timeBetweenBullets));
            foreach (Transform var in spawnLocList)
            {
                StartCoroutine(WaitToUnParentBullet(Instantiate(projectileType, var), timeToUnParent));
            }
        }
    }
    public virtual void Reload()
    {
        if (!IsReloading())
        {
            reloadingCoroutine = StartCoroutine(ReloadTimer());
        }
    }
    IEnumerator ReloadTimer()
    {
        float startTime = 0;
        while(startTime < reloadTime)
        {
            startTime += Time.deltaTime;
            currentAmmo = (int)Mathf.Lerp(currentAmmo, maxAmmo, startTime / reloadTime);
            Debug.Log("Reloading: "+currentAmmo);
            yield return new WaitForEndOfFrame();
        }
        reloadingCoroutine = null;
    }
    IEnumerator WaitToUnParentBullet(Projectile projectileToWait,float timeToUnParent)
    {
        yield return new WaitForSeconds(timeToUnParent);
        if(projectileToWait)
            projectileToWait.transform.parent = null;
    }

    IEnumerator WaitForNextShot(float maxTime)
    {
        float startTime = 0f;
        while(startTime < maxTime)
        {
            startTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        waitForNextShot = null;
    }

    bool IsReloading() { if (reloadingCoroutine == null) { return false; } else { return true; } }
    public void SetGunVisibility(bool isVis)
    {
        if (meshRender != null)
        {
            meshRender.enabled = isVis;
        }
        else { Debug.Log("meshRender does not exist"); }
    }
}
