using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] Projectile projectile;
    [SerializeField] Projectile secondaryProjectile;
    [SerializeField] Transform spawnLocation;


    private void Start()
    {
        SetProjectile(projectile);
        SetSecoundaryProjectile(secondaryProjectile);
        SetSpawnLoc(spawnLocation);
    }

    public override void Attack()
    {
        base.Attack();
        
    }

    public override void SecondaryAttack()
    {
        base.SecondaryAttack();
    }

    public override void Reload(MainCanvas mainCanvas,int currentWeaponSelected)
    {
        base.Reload(mainCanvas, currentWeaponSelected);
    }
}
