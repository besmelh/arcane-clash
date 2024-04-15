using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int health;
    public int maxHealth;
    //public int damage; 
    public float movementSpeed = 2;
    private float horInput;
    [SerializeField] FloatingHealthBar healthBar;


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
            Destroy(gameObject);
        }
    }
}
