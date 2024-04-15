using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //public Vector3 finalDestination;
    public int health; // current health
    public int maxHealth; // max possible health
    public int damage; // damage strength it causes to player
    public float movementSpeed; // speed at which it moves
    private PointManager pointManager; //manages the HP points of the player
    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();   
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        //since pointManager is only created on game start, link to that instance after game start
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    void Update()
    {
        //transform.Translate(new Vector3(0, 0, movementSpeed * -1));
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }

    public void UpdateHealth(int damage)
    {
        // reduce health
        health = health - damage;

        // update health bar
        healthBar.UpdateHealthBar(health, maxHealth);

        // delete if no health remaining
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    //update player HP - hitting player directly results in extra damage
        //    //pointManager.UpdateScore(-25);
        //}
        //if (other.gameObject.tag == "Projectile")
        //{
        //    // destroy projectile
        //    Destroy(other.gameObject);

        //    // reduce health
        //    health = health - 1;
        //    healthBar.UpdateHealthBar(health, maxHealth);

        //    // delete if no health remaining
        //    if (health <= 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
        if (other.gameObject.tag == "Boundary")
        {
            // prevent endless enemies from remaining in game
            Destroy(gameObject);

            //update player HP ones enemy reaches end of track
            pointManager.UpdateScore(-25);
        }
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
