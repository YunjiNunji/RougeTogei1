using System.Collections;
using UnityEngine;

public class AllOrNothing : MonoBehaviour
{
    public TurnManager turnManager; // Assign this in the inspector

    public void Activate(GameObject playerObj, GameObject enemyObj)
    {
        HealthDisplay player = playerObj.GetComponent<HealthDisplay>();
        HealthDisplay enemy = enemyObj.GetComponent<HealthDisplay>();

        float roll = Random.value;

        if (roll < 0.5f)
        {
            int damage = enemy.currentHealth;
            enemy.TakeDamage(damage);
            Debug.Log($"[All or Nothing] 🎯 Success! Rolled {roll:F2} — Enemy one-shotted ({damage} dmg).");
        }
        else
        {
            int damage = player.currentHealth;
            player.TakeDamage(damage);
            Debug.Log($"[All or Nothing] ❌ Failed! Rolled {roll:F2} — Player dies ({damage} dmg).");
        }

        turnManager.CheckEndGame(); // Check immediately after damage
    }
}


