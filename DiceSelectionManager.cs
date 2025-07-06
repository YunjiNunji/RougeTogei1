using UnityEngine;

public class DiceSelectionManager : MonoBehaviour
{
    public static DiceSelectionManager Instance;

    private DiceRollNew selectedDie;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public void SetSelectedDie(DiceRollNew die)
    {
        if (selectedDie != null)
            selectedDie.Deselect();

        selectedDie = die;
        Debug.Log($"Selected die with value: {die.dieValue}");
    }

    public void ClearSelectedDie()
    {
        selectedDie = null;
    }

    public DiceRollNew GetSelectedDie()
    {
        return selectedDie;
    }
}
