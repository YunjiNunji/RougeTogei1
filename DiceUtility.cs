using System.Collections.Generic;
using UnityEngine;

public static class DiceUtility
{
    public static int RollHighroller(int sides)
    {
        List<int> outcomes = new List<int>();

        for (int i = 1; i <= sides; i++)
        {
            int weight = (i == 1 || i == sides) ? 3 : 1;
            for (int j = 0; j < weight; j++)
                outcomes.Add(i);
        }

        return outcomes[Random.Range(0, outcomes.Count)];
    }

    public static int RollStandard(int sides)
    {
        return Random.Range(1, sides + 1);
    }
}
