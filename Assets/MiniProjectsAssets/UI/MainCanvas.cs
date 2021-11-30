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
    private GameObject _weaponUI;
    private GameObject parentObject;
    private int maxAmmoGiven;
    private List<List<GameObject>> weaponsUIList = new List<List<GameObject>>();

    public void SpawnWeaponUIAndInitNeededVariables(int maxAmmo,GameObject weaponUI)
    {
        maxAmmoGiven = maxAmmo;
        _weaponUI = weaponUI;
        SpawnWeaponUI();
    }
    public void UpdateShootBulletUI(int currentAmmo, int maxAmmo,int currentGunSelected)
    {
        for (int i = currentAmmo; i < maxAmmo; i++)
        {
            weaponsUIList[currentGunSelected][i].SetActive(true);
        }
       
    }

    public void UpdateReloadBulletUI(int currentAmmo, int maxAmmo,int currentGunSelected)
    {
        if (currentAmmo == maxAmmo)
        {
            weaponsUIList[currentGunSelected][currentAmmo-1].SetActive(false);
            return;
        }
        weaponsUIList[currentGunSelected][currentAmmo].SetActive(false);
    }

    public void SwapUIVisibility(int previousGunSelected,int currentGunSelected)
    {
        //Debug.Log(weaponsUIList[previousGunSelected][0].gameObject.transform.parent.gameObject);
        weaponsUIList[previousGunSelected][0].gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        weaponsUIList[currentGunSelected][0].gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
    }
    void SpawnWeaponUI()
    {

        if (_weaponUI != null)
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
            parentObject.transform.parent = originSpawnPoint.transform;
            parentObject.name = _weaponUI.ToString() + "UIParent";
            parentObject.transform.position = Vector3.zero;

            for (int i = 1; i < maxAmmoGiven + 1; i++)
            {
                GameObject newAmmoUIElement = Instantiate(_weaponUI);
                newAmmoUIElement.GetComponent<RectTransform>().SetParent(this.transform);
                
                Vector3 offset = new Vector3(originSpawnPoint.position.x * i, 0,0);
                newAmmoUIElement.GetComponent<RectTransform>().SetPositionAndRotation(offset,Quaternion.identity);
                
                newAmmoUIElement.GetComponent<RectTransform>().SetParent(parentObject.transform);
                ammoGameObjects.Add(newAmmoUIElement.GetComponent<BulletUIInfoHolder>().GetUnloadedGameObject());
            }
            
            weaponsUIList.Add(ammoGameObjects);
        }
        else{Debug.Log("Max Ammo Given is 0");}
    }
}
