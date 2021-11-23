using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventorySystem : MonoBehaviour
{
    [SerializeField] List<Weapon> tempWeapons = new List<Weapon>();
    [SerializeField] Transform weaponsSpawnLoc;
    private int currentWeaponSelection;
    private int _previousWeaponSelection;
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
        _mainCanvas.SpawnWeaponUIAndInitNeededVariables(weaponToAdd.GetMaxAmmo(),weaponToAdd.GetWeaponUI());
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
                _mainCanvas.SwapUIVisibility(_previousWeaponSelection,currentWeaponSelection);
            }
            else
            {
                var.SetGunVisibility(false);
                _mainCanvas.SwapUIVisibility(_previousWeaponSelection,currentWeaponSelection);
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
            ChangeWeaponToFirstSelection();
            return;
        }
        //regular weapon selection change
        _previousWeaponSelection = currentWeaponSelection;
        currentWeaponSelection = Mathf.Clamp(val + currentWeaponSelection, 0, tempWeapons.Count - 1);
        ChooseWeaponsVisibility();
    }

    public void ChangeWeaponToFirstSelection()
    {
        _previousWeaponSelection = currentWeaponSelection;
        currentWeaponSelection = 0;
        ChooseWeaponsVisibility();
    }
    public void ChangeWeaponToLastSelection()
    {
        _previousWeaponSelection = currentWeaponSelection;
        currentWeaponSelection = tempWeapons.Count - 1;
        ChooseWeaponsVisibility();
    }

    public void ReloadCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].Reload(_mainCanvas,currentWeaponSelection);
    }

    public void AttackPrimaryCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        if (!currentHeldWeapons[currentWeaponSelection].IsReloading())
        {
            currentHeldWeapons[currentWeaponSelection].Attack();
            _mainCanvas.UpdateShootBulletUI(currentHeldWeapons[currentWeaponSelection].GetCurrentAmmo(),
                currentHeldWeapons[currentWeaponSelection].GetMaxAmmo(), currentWeaponSelection);
        }
    }

    public void AttackSecondaryCurrentWeapon()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        if (!currentHeldWeapons[currentWeaponSelection].IsReloading())
        {
            currentHeldWeapons[currentWeaponSelection].SecondaryAttack();
            _mainCanvas.UpdateShootBulletUI(currentHeldWeapons[currentWeaponSelection].GetCurrentAmmo(),
                currentHeldWeapons[currentWeaponSelection].GetMaxAmmo(), currentWeaponSelection);
        }
    }
}
