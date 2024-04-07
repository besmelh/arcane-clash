using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{

    public GameObject projectilePrefab;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // object, translation, rotation (no rotation)
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
        
    }
}
