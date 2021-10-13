using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] Projectile projectile;
    [SerializeField] Projectile secondaryProjectile;
    [SerializeField] Transform[] spawnLocations;
    private void Start()
    {
        SetProjectile(projectile);
        SetSecoundaryProjectile(secondaryProjectile);

        foreach (Transform var in spawnLocations)
        {
            SetSpawnLoc(var);
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void SecondaryAttack()
    {
        base.SecondaryAttack();
    }

    public override void Reload()
    {
        base.Reload();
    }
}
