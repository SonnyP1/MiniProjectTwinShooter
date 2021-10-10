using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] Projectile Projectile;
    [SerializeField] Projectile SecoundaryProjectile;
    [SerializeField] Transform spawnLocation;


    private void Start()
    {
        SetProjectile(Projectile);
        SetSecoundaryProjectile(SecoundaryProjectile);
        SetSpawnLoc(spawnLocation);
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void SecoundaryAttack()
    {
        base.SecoundaryAttack();
    }

    public override void Reload()
    {
        base.Reload();
    }
}
