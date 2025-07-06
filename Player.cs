using UnityEngine;

public class Player : Character
{
    public ShadowStep shadowStep;
    public HealthDisplay healthDisplay;

    protected override void Start()
    {
        base.Start();
        characterName = "Highroller";

        if (shadowStep == null)
            shadowStep = GetComponent<ShadowStep>();

        if (healthDisplay == null)
            healthDisplay = GetComponent<HealthDisplay>();

        if (healthDisplay != null)
        {
            healthDisplay.maxHealth = 70;
            healthDisplay.characterName = "Player";
            healthDisplay.currentHealth = 70; // Ensures it overrides prefab default
        }
    }


    public void TakeDamage(int amount)
    {
        if (shadowStep != null)
        {
            amount = shadowStep.AbsorbDamage(amount);
        }

        if (amount > 0)
        {
            healthDisplay.TakeDamage(amount);
        }
        else
        {
            Debug.Log("?? All damage was absorbed by the Shadow Clone.");
        }
    }

    public void OnTurnStart()
    {
        if (shadowStep != null)
            shadowStep.TickCooldown();
    }

    public bool IsDead()
    {
        return healthDisplay != null && healthDisplay.currentHealth <= 0;
    }
}
