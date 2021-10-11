using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUpInteractable : Interactable
{
    PlayerScript playerWeaponInventory;
    BoxCollider gunInteractableTriggerBox;
    Weapon typeOfWeapon;
    private void Awake()
    {
        gunInteractableTriggerBox = gameObject.GetComponent<BoxCollider>();
    }
    public override void Interacting()
    {
        if (gunInteractableTriggerBox.enabled == true)
        {
            playerWeaponInventory.AddToWeaponList(typeOfWeapon);
            transform.position = playerWeaponInventory.GetWeaponSpawnLoc().position;
            transform.rotation = playerWeaponInventory.GetWeaponSpawnLoc().rotation;
            transform.parent = playerWeaponInventory.GetWeaponSpawnLoc();
            gunInteractableTriggerBox.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print(other.name);
            //needs fix after refactoring
            //playerWeaponInventory = transform.parent.GetComponent<PlayerScript>();
            if (playerWeaponInventory != null)
            {
                print("TRIGGER BOX IS REAL");
            }
            else { print("TRIGGER BOX IS NO REAL"); }
        }
    }
}
