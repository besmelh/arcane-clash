using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerDeath;
    public static PlayerController instance;

    private int health;
    public int maxHealth;
    //public int damage; 
    public float movementSpeed = 2;
    private float horInput;
    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);


    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horInput * movementSpeed * Time.deltaTime);
    }

    public void UpdateHealth(int healthDifference)
    {
        // reduce or increase health
        health = health + healthDifference;

        // update health bar
        healthBar.UpdateHealthBar(health, maxHealth);

        // end game if health 0
        if (health <= 0)
        {
            // Player has died
            OnPlayerDeath?.Invoke();
            // Disable or destroy the player GameObject
            gameObject.SetActive(false);
            // or
            // Destroy(gameObject);
        }
    }
}
