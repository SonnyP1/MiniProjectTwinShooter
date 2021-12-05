using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSpin : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Transform>().Rotate(0,rotSpeed,0,Space.Self);
    }
}
