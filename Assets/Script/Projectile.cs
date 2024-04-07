using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float movementSpeed;
    private PointManager pointManager;

    void Start()
    {
        //since pointManager is only created on game start, link to that instance after game start
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>(); 
    }

    // Update is called once per frame
    void Update() 
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // destroy enemy and projectile
            Destroy(other.gameObject);
            Destroy(gameObject);

            //update enemy HP
            // for testing - updating score
            pointManager.UpdateScore(50);
        }
        else if (other.gameObject.tag == "Boundary")
        {
            // prevent endless projectiles from remaining in game
            Destroy(gameObject);
        }
    }
}
