using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //public Vector3 finalDestination;
    public int health; // between 0 and 100
    public int damage; // between 0 and 100
    public float movementSpeed; // between 0 and 1

    void Update()
    {
        transform.Translate(new Vector3(0, 0, movementSpeed * -1));
    }
}
