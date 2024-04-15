using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{

    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // object, translation, rotation (no rotation)
            Instantiate(attack_1, transform.position, attack_1.transform.rotation);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            // object, translation, rotation (no rotation)
            Instantiate(attack_2, transform.position, attack_2.transform.rotation);
            //Instantiate(attack_2, transform.position, Quaternion.Euler(90f, 0f, 0f));
            //Debug.Log("rot: " + attack_2.transform.rotation);
            //Instantiate(attack_2, transform.position, Quaternion.identity);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            // object, translation, rotation (no rotation)
            Instantiate(attack_3, transform.position, Quaternion.identity);
        }

    }
}
