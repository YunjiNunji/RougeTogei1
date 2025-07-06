using UnityEngine;

public enum DiceRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Mythic
}

[System.Serializable]
public class Dice
{
    public string diceName;
    public int sides;
    public DiceRarity rarity;

    public Dice(string name, int sides, DiceRarity rarity)
    {
        this.diceName = name;
        this.sides = sides;
        this.rarity = rarity;
    }

    public override string ToString()
    {
        return $"{rarity} d{sides}";
    }
}
