using System.Collections.Generic;
using UnityEngine;

public class DiceStatsLogger : MonoBehaviour
{
    private Dictionary<int, int> rollCounts = new();
    public int sides = 6;

    void Start()
    {
        for (int i = 1; i <= sides; i++)
            rollCounts[i] = 0;
    }

    public void RecordRoll(int value)
    {
        if (!rollCounts.ContainsKey(value))
            rollCounts[value] = 0;

        rollCounts[value]++;
        PrintStats();
    }

    private void PrintStats()
    {
        Debug.Log("?? Dice Roll Stats:");
        for (int i = 1; i <= sides; i++)
        {
            Debug.Log($"Rolled {i}: {rollCounts[i]} time(s)");
        }
    }
}
