using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;

    public float rateOfFire1 = 0.5f; // Rate of fire for attack_1 in seconds
    public float rateOfFire2 = 0.5f; // Rate of fire for attack_2 in seconds
    public float rateOfFire3 = 0.5f; // Rate of fire for attack_3 in seconds

    private bool canFire1 = true; // Flag to control firing of attack_1
    private bool canFire2 = true; // Flag to control firing of attack_2
    private bool canFire3 = true; // Flag to control firing of attack_3

    private IEnumerator FireRoutine1()
    {
        while (true)
        {
            if (Input.GetButtonDown("Fire1") && canFire1)
            {
                Instantiate(attack_1, transform.position, attack_1.transform.rotation);
                canFire1 = false;
                yield return new WaitForSeconds(rateOfFire1);
                canFire1 = true;
            }
            yield return null;
        }
    }

    private IEnumerator FireRoutine2()
    {
        while (true)
        {
            if (Input.GetButtonDown("Fire2") && canFire2)
            {
                Instantiate(attack_2, transform.position, attack_2.transform.rotation);
                canFire2 = false;
                yield return new WaitForSeconds(rateOfFire2);
                canFire2 = true;
            }
            yield return null;
        }
    }

    private IEnumerator FireRoutine3()
    {
        while (true)
        {
            if (Input.GetButtonDown("Fire3") && canFire3)
            {
                Instantiate(attack_3, transform.position, Quaternion.identity);
                canFire3 = false;
                yield return new WaitForSeconds(rateOfFire3);
                canFire3 = true;
            }
            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(FireRoutine1());
        StartCoroutine(FireRoutine2());
        StartCoroutine(FireRoutine3());
    }
}
