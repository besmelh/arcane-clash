using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //public float movementSpeed;
    //private PointManager pointManager;

    void Start()
    {
        //since pointManager is only created on game start, link to that instance after game start
        //pointManager = GameObject.Find("PointManager").GetComponent<PointManager>(); 
    }

    // Update is called once per frame
    void Update() 
    {
        //transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Boundary")
        //{
        //    // prevent endless projectiles from remaining in game
        //    Destroy(gameObject);
        //}

        //if (other.gameObject.tag == "Enemy")
        //{
        //    //Projectile p = c.GetComponent<Projectile>()
        //    // prevent endless projectiles from remaining in game
        //    //Destroy(gameObject);
        //}
    }

    //private void checkForDestructables()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, 4f);
    //    foreach (Collider c in colliders)
    //    {
    //        if (c.GetComponent<Enemy>())
    //        {

    //        }
    //    }
    //}
}
