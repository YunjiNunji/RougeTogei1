using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI healthText;
    public RectTransform healthFill;
    public HealthBarUI healthBarUI;

    [Header("Stats")]
    public int maxHealth = 10;
    public int currentHealth;

    [Tooltip("This is the display name used in logs.")]
    public string characterName = "Character";

    private float originalWidth;
    private bool initializedExternally = false;

    void Start()
    {
        if (!initializedExternally)
            currentHealth = maxHealth;

        StartCoroutine(InitializeBar());
    }

    public void InitializeHealth(int max, int current, string name = "Character")
    {
        maxHealth = max;
        currentHealth = current;
        characterName = name;
        initializedExternally = true;
    }

    private System.Collections.IEnumerator InitializeBar()
    {
        yield return null; // Wait one frame for layout
        if (healthFill != null)
        {
            originalWidth = healthFill.rect.width;
            Debug.Log($"[{characterName}] Original pink bar width: {originalWidth}");
        }
        UpdateHealthDisplay();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"{characterName} took {amount} damage. HP left: {currentHealth}");
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Debug.Log($"{characterName} has been defeated!");
            TurnManager tm = FindObjectOfType<TurnManager>();
            if (tm != null) tm.CheckEndGame();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
            healthText.text = $"HP: {currentHealth} / {maxHealth}";

        float percent = (float)currentHealth / maxHealth;

        if (healthBarUI != null)
        {
            Debug.Log($"[{characterName}] Updating bar fill to {percent * 100}%");
            healthBarUI.SetHealthFraction(percent);
        }
        else
        {
            Debug.LogWarning($"[{characterName}] HealthBarUI reference not assigned!");
        }
    }
}
