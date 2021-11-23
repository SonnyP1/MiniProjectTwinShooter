using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUpInteractable : Interactable
{
    private WeaponInventorySystem playerWeaponInventory;
    private BoxCollider gunInteractableTriggerBox;
    private Weapon typeOfWeapon;
    private InteractComp _playerInteractComp;
    private void Awake()
    {
        gunInteractableTriggerBox = gameObject.GetComponent<BoxCollider>();
        typeOfWeapon = GetComponent<Weapon>();
    }
    public override void Interacting()
    {
        if (gunInteractableTriggerBox.enabled == true)
        {
            
            playerWeaponInventory.AddToWeaponList(typeOfWeapon);
            playerWeaponInventory.ChangeWeaponToLastSelection();
            transform.position = playerWeaponInventory.GetWeaponSpawnLoc().position;
            transform.rotation = playerWeaponInventory.GetWeaponSpawnLoc().rotation;
            transform.parent = playerWeaponInventory.GetWeaponSpawnLoc();
            _playerInteractComp.RemoveInteractable(this);
            gunInteractableTriggerBox.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerInteract"))
        {
            playerWeaponInventory = other.transform.parent.GetComponent<WeaponInventorySystem>();
            _playerInteractComp = other.transform.GetComponent<InteractComp>();
        }
    }
}
