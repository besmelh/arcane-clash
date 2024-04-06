using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int health;
    public int damage; 
    public float movementSpeed = 2;
    public float horInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horInput * movementSpeed * Time.deltaTime);
    }
}
