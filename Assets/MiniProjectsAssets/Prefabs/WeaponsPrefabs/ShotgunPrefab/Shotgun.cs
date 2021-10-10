using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] Projectile Projectile;
    [SerializeField] Projectile SecoundaryProjectile;
    [SerializeField] Transform[] spawnLocations;
    private void Start()
    {
        SetProjectile(Projectile);
        SetSecoundaryProjectile(SecoundaryProjectile);

        foreach (Transform var in spawnLocations)
        {
            SetSpawnLoc(var);
        }
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
