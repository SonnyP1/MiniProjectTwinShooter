using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUIInfoHolder : MonoBehaviour
{
    [SerializeField] private GameObject UnloadedLoaded;

    public GameObject GetUnloadedGameObject()
    {
        return UnloadedLoaded;
    }
}
