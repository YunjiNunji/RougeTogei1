using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HatTrickAbility : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI numberHolderText;

    private int? storedValue = null;

    // Called when the player chooses a die for Hat Trick
    public void AllocateHatTrick(int value)
    {
        if (storedValue.HasValue)
        {
            Debug.Log("Hat Trick already has a stored value.");
            return;
        }

        storedValue = value;
        UpdateNumberHolderDisplay();
        Debug.Log("Hat Trick stored value: " + value);
    }

    // Called when the player activates Hat Trick
    public int? ActivateHatTrick()
    {
        if (!storedValue.HasValue)
        {
            Debug.Log("No Hat Trick value stored.");
            return null;
        }

        int result = Mathf.Max(0, storedValue.Value - 1);
        Debug.Log($"Hat Trick activated: {storedValue.Value} - 1 = {result}");

        storedValue = null;
        UpdateNumberHolderDisplay();

        return result;
    }

    // Check if Hat Trick is ready
    public bool CanActivate() => storedValue.HasValue;

    private void UpdateNumberHolderDisplay()
    {
        numberHolderText.text = storedValue.HasValue ? storedValue.Value.ToString() : "-";
    }

    // ? Moved inside the class
    public void HighlightToggle(bool isActive)
    {
        if (numberHolderText != null)
        {
            numberHolderText.color = isActive ? Color.cyan : Color.white;
        }
    }
}
