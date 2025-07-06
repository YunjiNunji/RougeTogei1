using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShadowStep : MonoBehaviour
{
    public int shadowCloneHP = 0;
    public int cooldown = 0;
    private const int maxCooldown = 3;

    public bool TryActivateShadowStep(List<DiceRollNew> selectedDice)
    {
        if (cooldown > 0)
        {
            Debug.Log("? Shadow Step is on cooldown!");
            return false;
        }

        if (selectedDice == null || selectedDice.Count == 0)
        {
            Debug.Log("? No dice selected for Shadow Step.");
            return false;
        }

        shadowCloneHP = selectedDice.Sum(die => die.dieValue);
        cooldown = maxCooldown;

        Debug.Log($"?? Shadow Step activated! Shadow Clone HP: {shadowCloneHP}");

        foreach (var die in selectedDice)
        {
            die.Deselect();
        }

        return true;
    }

    public int AbsorbDamage(int incomingDamage)
    {
        if (shadowCloneHP > 0)
        {
            int absorbed = Mathf.Min(incomingDamage, shadowCloneHP);
            shadowCloneHP -= absorbed;

            Debug.Log($"??? Shadow Clone absorbed {absorbed} damage. Remaining HP: {shadowCloneHP}");

            int remainingDamage = incomingDamage - absorbed;
            if (remainingDamage > 0)
                Debug.Log($"?? {remainingDamage} damage exceeded the clone and will hit the player.");

            return remainingDamage;
        }

        return incomingDamage;
    }

    public void TickCooldown()
    {
        if (cooldown > 0)
        {
            cooldown--;
            Debug.Log($"? Shadow Step cooldown: {cooldown} turns remaining");
        }
    }

    public bool IsAvailable()
    {
        return cooldown <= 0;
    }
}
