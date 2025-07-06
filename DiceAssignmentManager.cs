using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AllocationType { Attack, Block, Mana }

public class DiceAssignmentManager : MonoBehaviour
{
    public List<int> rolledDice = new();
    public Dictionary<AllocationType, List<int>> assignedDice = new();

    void Awake()
    {
        foreach (AllocationType type in System.Enum.GetValues(typeof(AllocationType)))
            assignedDice[type] = new List<int>();
    }

    public void AssignToCategory(int value, AllocationType type)
    {
        // Remove from all first
        foreach (var list in assignedDice.Values)
            list.Remove(value);

        assignedDice[type].Add(value);
    }

    public int GetTotal(AllocationType type)
    {
        return assignedDice[type].Sum();
    }

    public void ClearAssignments()
    {
        rolledDice.Clear();
        foreach (var list in assignedDice.Values)
            list.Clear();
    }
}