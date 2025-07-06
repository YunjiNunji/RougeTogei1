using System.Collections;
using UnityEngine;

public class DiceRollNew : MonoBehaviour
{
    public TurnManager turnManager;
    public AudioClip rollSound;
    public GameObject particleEffectPrefab;
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.cyan;

    private AudioSource audioSource;
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isSelected = false;
    public bool hasBeenRolled = false;
    public int dieValue = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            originalColor = sr.color;
    }

    void OnMouseEnter()
    {
        if (!isSelected && sr != null)
            sr.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (!isSelected && sr != null)
            sr.color = originalColor;
    }

    void OnMouseDown()
    {
        if (!turnManager.IsPlayerTurn()) return;

        if (!turnManager.HasRolledYet() && !hasBeenRolled)
        {
            turnManager.PlayerRollViaClick();

            if (rollSound != null && audioSource != null)
                audioSource.PlayOneShot(rollSound);

            if (particleEffectPrefab != null)
                Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }
        else if (turnManager.HasRolledYet())
        {
            ToggleSelection();
        }
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        sr.color = isSelected ? selectedColor : originalColor;
    }

    public void Deselect()
    {
        isSelected = false;
        sr.color = originalColor;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetRolledValue(int value)
    {
        dieValue = value;
        hasBeenRolled = true;
        isSelected = false;
        sr.color = originalColor;

        Debug.Log($"{gameObject.name} rolled: {dieValue}");
    }

    public void ResetDieForNewTurn()
    {
        hasBeenRolled = false;
        isSelected = false;
        sr.color = originalColor;
        dieValue = 0;
    }
}
