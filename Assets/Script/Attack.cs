using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // show class in inspector

public class Attack
{
    public AttackType attackType; // name of attack
    public int damage; // damage it causes to enemy
    public bool isAOE; // true if it's an area of effect attack
    public int amountOfSlowingEnemy; // degree that it slows the enemy by, 0 if doesn't slow it
    public int rateOfFire; // how often it can be fired
}

public enum AttackType
{
    Attack_Fireball,
    Attack_Frost_Nova,
    Attack_Lightning_Bolt
}
