using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BladedShadow : MonoBehaviour, IAbility
{
    public TurnManager turnManager;

    public void Activate(GameObject player, GameObject enemy)
    {
        if (!turnManager.IsPlayerTurn())
        {
            Debug.Log("Not your turn.");
            return;
        }

        List<int> selectedValues = new();
        foreach (var die in FindObjectsOfType<DiceRollNew>())
        {
            if (die.IsSelected())
            {
                selectedValues.Add(die.dieValue);
                die.Deselect();
            }
        }

        if (selectedValues.Count == 0)
        {
            Debug.Log("No dice selected.");
            return;
        }

        int damage = turnManager.ResolveBladedShadowDamage(selectedValues);
        if (damage > 0)
        {
            var health = enemy.GetComponent<HealthDisplay>();
            health.TakeDamage(damage);
            Debug.Log($"Bladed Shadow dealt {damage} damage!");
        }

        turnManager.CheckEndGame();
        if (!IsGameOver(enemy))
            turnManager.EndPlayerTurn();
    }

    private bool IsGameOver(GameObject enemy)
    {
        var hp = enemy.GetComponent<HealthDisplay>();
        return hp.currentHealth <= 0;
    }
}
