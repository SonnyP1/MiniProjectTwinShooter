using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [Header("BulletManagementUI")] 
    [SerializeField] RectTransform originSpawnPoint;
    [SerializeField] GameObject[] prefabUIToSpawn;
    private Weapon.WeaponType _weaponType;
    private GameObject parentObject;
    private GameObject weaponUIToSpawn;
    private int maxAmmoGiven;
    private List<GameObject> ammoGameObjects = new List<GameObject>();

    private void Start()
    {
        Debug.Log(originSpawnPoint.position.x);
    }

    public void SetGivenMaxAmmo(int newGivenMaxAmmo) { maxAmmoGiven = newGivenMaxAmmo;}
    public void SetWeaponTypeToSpawnThenSpawn(Weapon.WeaponType newTypeOfWeapon)
    {
        _weaponType = newTypeOfWeapon;
        SpawnWeaponUI();
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

    public void ShootUpdateBulletUI(int currentAmmo)
    {
        int amtToCompare = Mathf.Abs(currentAmmo - maxAmmoGiven);
        for (int i = 0; i < amtToCompare; i++)
        {
            ammoGameObjects[i].SetActive(true);
        }
    }

    public void ReloadUpdateBulletUI(int currentAmmo)
    {
        int amtToCompare = Mathf.Clamp(Mathf.Abs(currentAmmo - maxAmmoGiven),0,ammoGameObjects.Count-1);
        Debug.Log(amtToCompare);
        ammoGameObjects[amtToCompare].SetActive(false);
    }
    void CreateUIForGun()
    {
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
            ShootUpdateBulletUI(maxAmmoGiven);
        }
        else{Debug.Log("Max Ammo Given is 0");}
    }
}
