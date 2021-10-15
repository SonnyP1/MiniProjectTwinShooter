using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventorySystem : MonoBehaviour
{
    [SerializeField] List<Weapon> tempWeapons = new List<Weapon>();
    [SerializeField] Transform weaponsSpawnLoc;
    private int currentWeaponSelection;
    private GameObject prefabUIToAdd;

    public Transform GetWeaponSpawnLoc() { return weaponsSpawnLoc; }

    public bool IsWeaponListEmpty() { if (tempWeapons.Count == 0) { return true; }else { return false; } }

    private void Start()
    {
        ChooseWeaponsVisibility();
    }

    public void AddToWeaponList(Weapon weaponToAdd)
    {
        foreach (Weapon weapon in tempWeapons)
        {
            if (weaponToAdd == weapon)
            {
                //add ammo to weapon;
                return;
            }
        }

        weaponToAdd.SetCurrentWeaponSelection(currentWeaponSelection);
        weaponToAdd.GiveTheUIMaxAmmoForCurrentWeapon();
        weaponToAdd.SpawnWeaponUI();
        tempWeapons.Add(weaponToAdd);
    }
    
    void ChooseWeaponsVisibility()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        foreach (Weapon var in tempWeapons)
        {
            if(var == currentHeldWeapons[currentWeaponSelection])
            {
                var.SetGunVisibility(true);
                var.GetMainCanvas().UpdateWeaponUISelection(currentWeaponSelection,true);
            }
            else
            {
                Debug.Log("TURN OFF THIS WEAPON: "+currentHeldWeapons[currentWeaponSelection].name);
                var.SetGunVisibility(false);
                var.GetMainCanvas().UpdateWeaponUISelection(currentWeaponSelection,false);
            }
            var.SetCurrentWeaponSelection(currentWeaponSelection);
        }
    }
    
    public void ChangeCurrentWeaponSelectionAndVisibility(int val)
    {
        if (currentWeaponSelection.Equals(0) && val == -1)
        {
            currentWeaponSelection = tempWeapons.Count - 1;
            ChooseWeaponsVisibility();
            return;
        }
        if (currentWeaponSelection.Equals(tempWeapons.Count - 1) && val == 1)
        {
            currentWeaponSelection = 0;
            ChooseWeaponsVisibility();
            return;
        }
        currentWeaponSelection = Mathf.Clamp(val + currentWeaponSelection, 0, tempWeapons.Count - 1);
        ChooseWeaponsVisibility();
    }
    
    public void ChangeWeaponToLastSelection()
    {
        currentWeaponSelection = tempWeapons.Count - 1;
        ChooseWeaponsVisibility();
    }

    public void ReloadCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].Reload();
    }

    public void AttackPrimaryCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].Attack();
    }

    public void AttackSecondaryCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].SecondaryAttack();
    }
}
