using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class AbilitiesController : MonoBehaviour
{
    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;
    public float rateOfFire1 = 0.5f; // Rate of fire for attack_1 in seconds
    public float rateOfFire2 = 0.5f; // Rate of fire for attack_2 in seconds
    public float rateOfFire3 = 0.5f; // Rate of fire for attack_3 in seconds

    private int activeAttackIndex = 0;
    private bool[] canFire = new bool[3];
    private Dictionary<int, Coroutine> fireCoroutines = new Dictionary<int, Coroutine>();
    private AttackUI attackUI;

    private void Start()
    {
        attackUI = FindObjectOfType<AttackUI>();
        if (attackUI != null)
        {
            attackUI.SetActiveAttack(activeAttackIndex);
        }

        for (int i = 0; i < canFire.Length; i++)
        {
            canFire[i] = true;
        }

        // Create object pools for attack projectiles
        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.CreatePool("Attack1", attack_1, 10);
            ObjectPool.Instance.CreatePool("Attack2", attack_2, 10);
            ObjectPool.Instance.CreatePool("Attack3", attack_3, 10);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ChangeActiveAttack(0);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            ChangeActiveAttack(1);
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            ChangeActiveAttack(2);
        }

        if (Input.GetButtonDown("Jump") && canFire[activeAttackIndex])
        {
            FireAttack();
        }

        if (attackUI != null)
        {
            attackUI.UpdateCooldowns(canFire);
        }
    }

    public int GetActiveAttackIndex()
    {
        return activeAttackIndex;
    }

    private void ChangeActiveAttack(int index)
    {
        activeAttackIndex = index;
        if (attackUI != null)
        {
            attackUI.SetActiveAttack(activeAttackIndex);
        }
    }

    private void FireAttack()
    {
        string attackTag = $"Attack{activeAttackIndex + 1}";
        GameObject attackPrefab;
        float rateOfFire;
        switch (activeAttackIndex)
        {
            case 0:
                attackPrefab = attack_1;
                rateOfFire = rateOfFire1;
                break;
            case 1:
                attackPrefab = attack_2;
                rateOfFire = rateOfFire2;
                break;
            case 2:
                attackPrefab = attack_3;
                rateOfFire = rateOfFire3;
                break;
            default:
                return;
        }

        // Get the attack projectile from the object pool
        GameObject attackProjectile = ObjectPool.Instance.GetObjectFromPool(attackTag);

        if (attackProjectile != null)
        {
            attackProjectile.transform.position = transform.position;
            attackProjectile.transform.rotation = attackPrefab.transform.rotation;
        }

        Instantiate(attackPrefab, transform.position, attackPrefab.transform.rotation);
        canFire[activeAttackIndex] = false;

        if (fireCoroutines.ContainsKey(activeAttackIndex))
        {
            StopCoroutine(fireCoroutines[activeAttackIndex]);
        }

        fireCoroutines[activeAttackIndex] = StartCoroutine(ResetFire(activeAttackIndex, rateOfFire));
    }

    private IEnumerator ResetFire(int attackIndex, float rateOfFire)
    {
        yield return new WaitForSeconds(rateOfFire);
        canFire[attackIndex] = true;
        fireCoroutines.Remove(attackIndex);
    }
}