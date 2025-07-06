using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold = 0;
    public List<Dice> diceInventory = new List<Dice>();
    public List<Upgrade> upgrades = new List<Upgrade>();
    public int actProgress = 0;
}

