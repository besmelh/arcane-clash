using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour
{
    public Image[] attackCircles;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.white;
    public Color cooldownColor = Color.red;

    public void SetActiveAttack(int index)
    {
        for (int i = 0; i < attackCircles.Length; i++)
        {
            attackCircles[i].color = (i == index) ? activeColor : inactiveColor;
        }
    }

    public void UpdateCooldowns(bool[] canFire)
    {
        for (int i = 0; i < attackCircles.Length; i++)
        {
            if (!canFire[i])
            {
                attackCircles[i].color = cooldownColor;
            }
            else if (i == FindObjectOfType<AbilitiesController>().GetActiveAttackIndex())
            {
                attackCircles[i].color = activeColor;
            }
            else
            {
                attackCircles[i].color = inactiveColor;
            }
        }
    }
}