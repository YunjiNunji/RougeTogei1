using UnityEngine;

public class Combatant : MonoBehaviour
{
    public string combatantName = "Unknown";
    public int maxHealth = 10;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{combatantName} took {amount} damage. HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"{combatantName} has been defeated!");
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}