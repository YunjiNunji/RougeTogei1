using UnityEngine;

public abstract class Enemy : Character
{
    protected override void Start()
    {
        base.Start();
    }

    public abstract void PerformAction(Player player);

    public bool IsDead()
    {
        HealthDisplay health = GetComponent<HealthDisplay>();
        return health != null && health.currentHealth <= 0;
    }
}
