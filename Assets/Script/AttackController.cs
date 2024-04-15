using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public float movementSpeed;
    //private PointManager pointManager;
    public int damage; // damage amount it causes to enemy
    public bool isAOE; // true if it's an area of effect attack
    public int slowingImpact; // degree that it slows the enemy by, 0 if doesn't slow it
    public int rateOfFire; // how often it can be fired

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move attack projectile
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // destroy attack projectile
            Destroy(gameObject);

            // update the enemy's health
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.UpdateHealth(damage);
            }
        }
        else if (other.gameObject.tag == "Boundary")
        {
            // prevent endless projectiles from remaining in game
            Destroy(gameObject);
        }
    }

}