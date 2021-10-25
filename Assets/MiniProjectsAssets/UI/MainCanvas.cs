using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [Header("BulletUIManagement")] 
    [SerializeField] RectTransform originSpawnPoint;
    [SerializeField] GameObject[] prefabUIToSpawn;
    private Weapon.WeaponType _weaponType;
    private GameObject parentObject;
    private GameObject weaponUIToSpawn;
    private int maxAmmoGiven;
    private List<List<GameObject>> weaponsUIList = new List<List<GameObject>>();

    public void SpawnWeaponUIAndInitNeededVariables(int maxAmmo,Weapon.WeaponType newTypeOfWeapon)
    {
        maxAmmoGiven = maxAmmo;
        _weaponType = newTypeOfWeapon;
        SpawnWeaponUI();
    }
    public void UpdateBulletUI(int currentAmmo, int currentGunSelected)
    {
        int amtToCompare = Mathf.Abs(currentAmmo - maxAmmoGiven);
        for (int i = 0; i < amtToCompare; i++)
        {
            weaponsUIList[currentGunSelected][i].SetActive(true);
        }
    }
    void SpawnWeaponUI()
    {
        switch (_weaponType)
        {
            case Weapon.WeaponType.Pistol:
                weaponUIToSpawn = prefabUIToSpawn[0];
                break;
            case Weapon.WeaponType.Shotgun:
                weaponUIToSpawn = prefabUIToSpawn[1];
                break;
            default:
                Debug.Log("Something with wrong!");
                break;
        }

        if (weaponUIToSpawn != null)
        {
            CreateUIForGun();
        }
        else
        {
            Debug.Log("WeaponUIToSpawn is NULL");
        }
    }


    void CreateUIForGun()
    {
        List<GameObject> ammoGameObjects = new List<GameObject>();
        if (maxAmmoGiven != 0)
        {
            parentObject = new GameObject();
            parentObject.transform.parent = this.transform;
            parentObject.name = _weaponType.ToString() + "UIParent";
            parentObject.transform.position = Vector3.zero;
            
            for (int i = 1; i < maxAmmoGiven + 1; i++)
            {
                GameObject newAmmoUIElement = Instantiate(weaponUIToSpawn);
                newAmmoUIElement.GetComponent<RectTransform>().SetParent(this.transform);
                
                Vector3 offset = new Vector3(originSpawnPoint.position.x * i, originSpawnPoint.position.y, originSpawnPoint.position.z);
                newAmmoUIElement.GetComponent<RectTransform>().SetPositionAndRotation(offset,Quaternion.identity);
                
                newAmmoUIElement.GetComponent<RectTransform>().SetParent(parentObject.transform);
                ammoGameObjects.Add(newAmmoUIElement.GetComponent<BulletUIInfoHolder>().GetUnloadedGameObject());
            }
            
            ammoGameObjects.Reverse();
            weaponsUIList.Add(ammoGameObjects);
        }
        else{Debug.Log("Max Ammo Given is 0");}
    }
}
