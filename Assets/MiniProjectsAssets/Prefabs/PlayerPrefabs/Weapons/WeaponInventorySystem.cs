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
    private MainCanvas _mainCanvas;

    public Transform GetWeaponSpawnLoc() { return weaponsSpawnLoc; }

    public bool IsWeaponListEmpty() { if (tempWeapons.Count == 0) { return true; }else { return false; } }

    private void Start()
    {
        _mainCanvas = FindObjectOfType<MainCanvas>();
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
        _mainCanvas.SpawnWeaponUIAndInitNeededVariables(weaponToAdd.GetMaxAmmo(),weaponToAdd.GetWeaponType());
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
            }
            else
            {
                Debug.Log("TURN OFF THIS WEAPON: "+currentHeldWeapons[currentWeaponSelection].name);
                var.SetGunVisibility(false);
            }
        }
    }
    
    public void ChangeCurrentWeaponSelectionAndVisibility(int val)
    {
        //check if the first one if scrolls down go to last one
        if (currentWeaponSelection.Equals(0) && val == -1)
        {
            ChangeWeaponToLastSelection();
            return;
        }
        //check if the last one if scrolls up go to the first one
        if (currentWeaponSelection.Equals(tempWeapons.Count - 1) && val == 1)
        {
            currentWeaponSelection = 0;
            ChooseWeaponsVisibility();
            return;
        }
        //regular weapon selection change
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
        /* This CRASHES UNITY
        while (currentHeldWeapons[currentWeaponSelection].GetReloadingCoroutine() != null)
        {
            _mainCanvas.UpdateBulletUI(currentHeldWeapons[currentWeaponSelection].GetCurrentAmmo(),currentWeaponSelection);
        }
        */
    }

    public void AttackPrimaryCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].Attack();
        _mainCanvas.UpdateBulletUI(currentHeldWeapons[currentWeaponSelection].GetCurrentAmmo(),currentWeaponSelection);
    }

    public void AttackSecondaryCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].SecondaryAttack();
        _mainCanvas.UpdateBulletUI(currentHeldWeapons[currentWeaponSelection].GetCurrentAmmo(),currentWeaponSelection);
    }
}
