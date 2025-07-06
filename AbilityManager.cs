using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public TurnManager turnManager;

    public AllOrNothing allOrNothingAbility;
    public BladedShadow bladedShadowAbility;
    public HatTrickAbility hatTrickAbility;
    public ShadowStep shadowStepAbility;

    private List<DiceRollNew> usedDice = new();
    private bool hatTrickToggledForUse = false;

    public void UseAllOrNothing()
    {
        if (turnManager.IsPlayerTurn())
        {
            allOrNothingAbility.Activate(player, enemy);
            CheckEndTurn();
            turnManager.CheckEndGame();
        }
        else
        {
            Debug.Log("It's not your turn!");
        }
    }

    public void UseBladedShadow()
    {
        if (!turnManager.IsPlayerTurn()) return;

        List<int> valuesUsed = new();
        foreach (var die in FindObjectsOfType<DiceRollNew>())
        {
            if (die.IsSelected() && !usedDice.Contains(die))
            {
                valuesUsed.Add(die.dieValue);
                MarkDieUsed(die);
            }
        }

        if (hatTrickToggledForUse && hatTrickAbility.CanActivate())
        {
            int? hatValue = hatTrickAbility.ActivateHatTrick();
            if (hatValue.HasValue)
            {
                valuesUsed.Add(hatValue.Value);
                Debug.Log($"Hat Trick value {hatValue.Value} added to ability combo.");
            }
            hatTrickToggledForUse = false;
        }

        if (valuesUsed.Count == 0)
        {
            Debug.Log("No dice selected for Bladed Shadow.");
            return;
        }

        int damage = turnManager.ResolveBladedShadowDamage(valuesUsed);
        enemy.GetComponent<HealthDisplay>().TakeDamage(damage);

        turnManager.CheckEndGame();
        CheckEndTurn();
    }

    public void UseHatTrick()
    {
        if (!turnManager.IsPlayerTurn()) return;

        foreach (var die in FindObjectsOfType<DiceRollNew>())
        {
            if (die.IsSelected() && !usedDice.Contains(die))
            {
                hatTrickAbility.AllocateHatTrick(die.dieValue);
                MarkDieUsed(die);
                die.Deselect();
                DiceSelectionManager.Instance.ClearSelectedDie();
                CheckEndTurn();
                return;
            }
        }

        Debug.Log("No die selected for Hat Trick.");
    }

    public void UseShadowStep()
    {
        if (!turnManager.IsPlayerTurn()) return;

        List<DiceRollNew> selectedDice = FindObjectsOfType<DiceRollNew>()
            .Where(die => die.IsSelected() && !usedDice.Contains(die))
            .ToList();

        if (selectedDice.Count == 0)
        {
            Debug.Log("No dice selected for Shadow Step.");
            return;
        }

        bool activated = shadowStepAbility.TryActivateShadowStep(selectedDice);

        if (!activated)
            return; // ? Do NOT end turn or consume dice

        foreach (var die in selectedDice)
        {
            MarkDieUsed(die);
        }

        CheckEndTurn();
    }

    public void ToggleHatTrickUsage()
    {
        if (!turnManager.IsPlayerTurn()) return;

        if (hatTrickAbility.CanActivate())
        {
            hatTrickToggledForUse = !hatTrickToggledForUse;
            hatTrickAbility.HighlightToggle(hatTrickToggledForUse);
        }
    }

    private void MarkDieUsed(DiceRollNew die)
    {
        if (!usedDice.Contains(die))
        {
            usedDice.Add(die);
            die.Deselect();
        }
    }

    private void CheckEndTurn()
    {
        bool allUsed = true;

        foreach (var die in FindObjectsOfType<DiceRollNew>())
        {
            if (die.hasBeenRolled && !usedDice.Contains(die))
            {
                allUsed = false;
                break;
            }
        }

        if (allUsed)
        {
            usedDice.Clear();
            turnManager.CheckEndGame();
            turnManager.EndPlayerTurn();
        }
    }
}
