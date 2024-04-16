using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyController : MonoBehaviour
{


    public EnemyType enemyType;
    public int scoreValue; // how much score it will add to player

    private int health; // current health
    public int maxHealth; // max possible health
    public int damageToPlayer; // damage strength it causes to player
    public float originalSpeed; // speed at which it normaly moves
    public Color normalHealthbarColor;
    public Color frozenHealthbarColor;
    private float currentSpeed; // speed at which it moves currently
    [SerializeField] FloatingHealthBar healthBar;


    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();

        // Set the default value of Healthbar colors
        ColorUtility.TryParseHtmlString("#4ECF4D", out normalHealthbarColor);
        ColorUtility.TryParseHtmlString("#00F2FF", out frozenHealthbarColor);
    }

    void Start()
    {
        currentSpeed = originalSpeed;
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        healthBar.ChangeHealthBarColor(normalHealthbarColor);
        if (PlayerController.instance != null)
        {
            PlayerController.instance.OnPlayerDeath += HandlePlayerDeath;
        }

    }

    void Update()
    {
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
    }

    private void DestroyEnemy()
    {
        // Notify the EnemySpawner script about the enemy's destruction
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();

        if (spawner != null)
        {
            spawner.HandleEnemyDestruction(this);
        }

        // Destroy the enemy game object
        Destroy(gameObject);
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
            //Destroy(gameObject);
            DestroyEnemy();
        }
    }

    public void SlowDown(float slowingImpact, float slowingDuration)
    {
        // set new reduced speed -- make sure it's not less than 0.05
        currentSpeed = Math.Max(0.1f, currentSpeed - slowingImpact);

        // change health bar color
        healthBar.ChangeHealthBarColor(frozenHealthbarColor);

        // return speed and health bar color back to normal after a while
        StartCoroutine(ReturnSpeedToNormal(slowingImpact, slowingDuration));

    }

    private IEnumerator ReturnSpeedToNormal(float slowingImpact, float slowingDuration)
    {
        yield return new WaitForSeconds(slowingDuration);

        // add back the speed that was decreased
        // this is implemented because multiple shots will add up 
        // shouldn't happen, but ensure that the original speed is not exceeded
        currentSpeed = Math.Min(originalSpeed, currentSpeed + slowingImpact);

        // change health bar color to normal
        healthBar.ChangeHealthBarColor(normalHealthbarColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            // prevent endless enemies from remaining in game
            //Destroy(gameObject);
            DestroyEnemy();

            //update player HP ones enemy reaches end of track
            PlayerController.instance.UpdateHealth(-damageToPlayer);
        }
    }

    private void OnDestroy()
    {
        if (PlayerController.instance != null)
        {
            PlayerController.instance.OnPlayerDeath -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        // stop moving or disable the enemy
        currentSpeed = 0f;
        enabled = false;
    }
}
