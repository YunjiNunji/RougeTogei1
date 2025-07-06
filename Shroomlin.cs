using UnityEngine;

public class Shroomlin : Enemy
{
    private int turnCount = 0;
    private int sporesRemaining = 0;

    protected override void Start()
    {
        base.Start();

        HealthDisplay health = GetComponent<HealthDisplay>();
        if (health != null)
        {
            health.InitializeHealth(40, 40, "Shroomlin");
        }
    }

    public override void PerformAction(Player player)
    {
        turnCount++;

        if (sporesRemaining > 0)
        {
            BurstSpore(player);
        }

        if (turnCount == 1 || (sporesRemaining == 0 && turnCount > 1 && (turnCount - 1) % 4 == 0))
        {
            FungalBurst();
        }
        else
        {
            MushRush(player);
        }
    }

    private void FungalBurst()
    {
        Debug.Log("Shroomlin activates Fungal Burst! ??");
        sporesRemaining = 3;
    }

    private void BurstSpore(Player player)
    {
        Debug.Log("A spore bursts on the player! ??");
        player.TakeDamage(6);
        sporesRemaining--;
        Debug.Log($"Spores remaining: {sporesRemaining}");
    }

    private void MushRush(Player player)
    {
        Debug.Log("Shroomlin uses Mush Rush! ??");
        player.TakeDamage(4);
    }
}
