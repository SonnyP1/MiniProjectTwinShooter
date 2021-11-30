using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] GameObject[] heartContainers;
    [SerializeField] Image[] heartFills;
    private int _maxHealth;
    public void SetMaxHealth(int newMaxHealth)
    {
        _maxHealth = newMaxHealth;
    }
    
    public void HeartContainersUpdate()
    {
        //needed later prob when adding Max Health through power up
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < _maxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }


    public void UpdateHeartFillContainers(int playerCurrentHealth)
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < playerCurrentHealth)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }
    }
}
