using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //public Vector3 finalDestination;
    public int health; // between 0 and 100
    public int damage; // between 0 and 100
    public float movementSpeed; // between 0 and 1
    private PointManager pointManager;

    void Start()
    {
        //since pointManager is only created on game start, link to that instance after game start
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    void Update()
    {
        //transform.Translate(new Vector3(0, 0, movementSpeed * -1));
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit!");
            // destroy enemy and projectile
            //Destroy(other.gameObject);
            //Destroy(gameObject);

            //update enemy HP
            // for testing - updating score
            pointManager.UpdateScore(-25);
        }
        else if (other.gameObject.tag == "Boundary")
        {
            // prevent endless projectiles from remaining in game
            Destroy(gameObject);
            pointManager.UpdateScore(-25);
        }
    }
}
