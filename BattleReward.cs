using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    Gold,
    Dice,
    Upgrade
}

[System.Serializable]
public class Reward
{
    public RewardType type;
    public int goldAmount;
    public Dice diceReward;
    public Upgrade upgradeReward;

    public string GetDescription()
    {
        switch (type)
        {
            case RewardType.Gold:
                return $"Gold: +{goldAmount}";
            case RewardType.Dice:
                return $"Dice: {diceReward.rarity} d{diceReward.sides}";
            case RewardType.Upgrade:
                return $"Upgrade: {upgradeReward.upgradeName}";
            default:
                return "Unknown Reward";
        }
    }
}

public class BattleReward : MonoBehaviour
{
    /// <summary>
    /// Generates 3 unique rewards: gold, a random dice, and a random upgrade.
    /// </summary>
    public List<Reward> GenerateRewardChoices(int actLevel)
    {
        List<Reward> choices = new List<Reward>();

        // Reward 1: Gold
        Reward gold = new Reward
        {
            type = RewardType.Gold,
            goldAmount = Random.Range(30, 51)
        };

        // Reward 2: Dice
        Dice dice = DiceFactory.GenerateDice(actLevel);
        Reward diceReward = new Reward
        {
            type = RewardType.Dice,
            diceReward = dice
        };

        // Reward 3: Upgrade
        Upgrade upgrade = UpgradeFactory.GenerateUpgrade(actLevel);
        Reward upgradeReward = new Reward
        {
            type = RewardType.Upgrade,
            upgradeReward = upgrade
        };

        choices.Add(gold);
        choices.Add(diceReward);
        choices.Add(upgradeReward);

        return choices;
    }

    /// <summary>
    /// Applies the selected reward to the player's state.
    /// </summary>
    public void ApplyReward(Reward reward)
    {
        switch (reward.type)
        {
            case RewardType.Gold:
                PlayerState.Instance.playerData.gold += reward.goldAmount;
                break;

            case RewardType.Dice:
                if (reward.diceReward != null)
                    PlayerState.Instance.playerData.diceInventory.Add(reward.diceReward);
                break;

            case RewardType.Upgrade:
                if (reward.upgradeReward != null)
                    PlayerState.Instance.playerData.upgrades.Add(reward.upgradeReward);
                break;
        }
    }
}
