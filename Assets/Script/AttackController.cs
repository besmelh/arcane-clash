using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public float movementSpeed;
    //private PointManager pointManager;
    public int damage; // damage amount it causes to enemy
    public bool isAOE; // true if it's an area of effect attack
    public float aoeRadius = 2f; // true if it's an area of effect attack
    public float slowingImpact; // degree that it slows the enemy by, 0 if doesn't slow it

    //public float rateOfFire; // how often it can be fired in seconds
    //private float nextFireTime; // Time when the attack can be fired again

    //public GameObject aoeVisualizerPrefab; // Reference to the AOE visualizer prefab
    //public float aoeVisualizerDuration = 0.5f; // Duration for which the AOE visualizer remains visible


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
            if (isAOE)
            {
                // Get the collision point
                Vector3 impactPosition = other.ClosestPointOnBounds(transform.position);

                // Apply AOE effect
                ApplyAOEEffect(impactPosition, aoeRadius);
            }
            else
            {
                // Apply single target effect
                ApplySingleTargetEffect(other.gameObject);
            }

            // destroy attack projectile
            //Destroy(gameObject, 0.1f);

            // Return the attack projectile to the object pool
            ObjectPool.Instance.ReturnObjectToPool(gameObject.tag, gameObject);

        }
        else if (other.gameObject.tag == "Boundary")
        {
            // prevent endless projectiles from remaining in game
            //Destroy(gameObject);

            // Return the attack projectile to the object pool
            ObjectPool.Instance.ReturnObjectToPool(gameObject.tag, gameObject);
        }
    }

    private void ApplyAOEEffect(Vector3 impactPosition, float aoeRadius)
    {
        // Find all colliders within the AOE radius
        Collider[] colliders = Physics.OverlapSphere(impactPosition, aoeRadius);

        // Instantiate the AOE visualizer prefab at the impact position
        //GameObject aoeVisualizer = Instantiate(aoeVisualizerPrefab, impactPosition, Quaternion.identity);

        //// Align the AOE visualizer with the ground
        //RaycastHit hit;
        //if (Physics.Raycast(impactPosition, Vector3.down, out hit))
        //{
        //    aoeVisualizer.transform.position = hit.point + Vector3.up * 0.1f;
        //    aoeVisualizer.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        //}

        //// Set the radius of the AOE visualizer
        //AOEVisualizerController aoeVisualizerController = aoeVisualizer.GetComponent<AOEVisualizerController>();
        //if (aoeVisualizerController != null)
        //{
        //    aoeVisualizerController.SetRadius(aoeRadius);
        //}

        // Apply the AOE effect to enemies within the radius
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                ApplySingleTargetEffect(collider.gameObject);
            }
        }

        // Destroy the AOE visualizer after the specified duration
        //Destroy(aoeVisualizer, aoeVisualizerDuration);
    }

    private void ApplySingleTargetEffect(GameObject enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            // Update the enemy's health
            enemyController.UpdateHealth(damage);

            // Update the enemy's speed if needed
            if (slowingImpact > 0)
            {
                enemyController.SlowDown(slowingImpact, 2f);
            }
        }
    }

}