using UnityEngine;

public static class DiceFactory
{
    public static Dice GenerateDice(int actLevel)
    {
        // Random sides from 4, 6, 8, 10, or 12
        int[] possibleSides = new int[] { 4, 6, 8, 10, 12 };
        int sides = possibleSides[Random.Range(0, possibleSides.Length)];

        // Choose rarity based on chance
        DiceRarity rarity = RollRarity();

        // Name it like "Epic d8"
        string name = $"{rarity} d{sides}";

        // Construct the Dice using your constructor
        return new Dice(name, sides, rarity);
    }

    private static DiceRarity RollRarity()
    {
        float roll = Random.value;

        if (roll < 0.05f) return DiceRarity.Mythic;    // 5% window
        if (roll < 0.15f) return DiceRarity.Epic;      // 10% window (0.05–0.15)
        if (roll < 0.40f) return DiceRarity.Rare;      // 25% window (0.15–0.40)
        return DiceRarity.Common;                      // 60% remaining
    }
}


