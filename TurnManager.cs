using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    enum Turn { Player, Enemy }
    Turn currentTurn = Turn.Player;

    public DiceRoller diceRoller;
    public Player player;
    public HealthDisplay playerHealth;
    public HealthDisplay enemyHealth;
    public Enemy currentEnemy;

    public enum AllocationType { Attack, Block, Mana }

    private Dictionary<AllocationType, List<int>> assignedDice = new();
    private bool playerHasRolled = false;
    private bool gameEnded = false;

    void Start()
    {
        foreach (AllocationType type in System.Enum.GetValues(typeof(AllocationType)))
        {
            assignedDice[type] = new List<int>();
        }

        Debug.Log("Player's Turn! Click the die to roll.");
    }

    void Update()
    {
        if (player == null || enemyHealth == null || playerHealth == null || currentEnemy == null)
            return;

        if (gameEnded)
            return;

        CheckEndGame();
        if (gameEnded)
            return;

        if (currentTurn == Turn.Enemy)
        {
            if (player.IsDead() || currentEnemy.IsDead())
            {
                CheckEndGame();
                return;
            }

            Debug.Log("👹 Enemy's Turn...");
            currentEnemy.PerformAction(player);

            CheckEndGame();

            if (!gameEnded)
            {
                Invoke(nameof(SwitchToPlayerTurn), 2f);
                currentTurn = Turn.Player;
            }
        }
    }

    void SwitchToEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        playerHasRolled = false;
        Debug.Log("Enemy's Turn...");
    }

    void SwitchToPlayerTurn()
    {
        currentTurn = Turn.Player;
        playerHasRolled = false;
        Debug.Log("Player's Turn! Click the die to roll.");

        foreach (var die in FindObjectsOfType<DiceRollNew>())
        {
            die.ResetDieForNewTurn();
        }

        player.OnTurnStart();
    }

    public void PlayerRollViaClick()
    {
        if (currentTurn != Turn.Player || playerHasRolled || gameEnded)
            return;

        diceRoller.RollAllDice();
        playerHasRolled = true;
    }

    public void AssignDieToCategory(int dieValue, AllocationType type)
    {
        foreach (var list in assignedDice.Values)
        {
            list.Remove(dieValue);
        }

        assignedDice[type].Add(dieValue);
        Debug.Log($"🎲 Assigned {dieValue} to {type}");
    }

    public void ConfirmAssignments()
    {
        if (!playerHasRolled || currentTurn != Turn.Player)
        {
            Debug.LogWarning("You must roll before confirming.");
            return;
        }

        int totalAttack = assignedDice[AllocationType.Attack].Sum();
        int totalBlock = assignedDice[AllocationType.Block].Sum();
        int totalMana = assignedDice[AllocationType.Mana].Sum();

        Debug.Log($"✅ Confirmed — Attack: {totalAttack}, Block: {totalBlock}, Mana: {totalMana}");

        enemyHealth.TakeDamage(totalAttack);

        foreach (var list in assignedDice.Values)
        {
            list.Clear();
        }

        CheckEndGame();

        if (!gameEnded)
        {
            Invoke(nameof(SwitchToEnemyTurn), 2f);
        }
    }

    public void CheckEndGame()
    {
        if (gameEnded) return;

        if (player.IsDead())
        {
            Debug.Log("💀 Enemy wins!");
            gameEnded = true;
            CancelInvoke();
        }
        else if (currentEnemy.IsDead())
        {
            Debug.Log("🎉 Player wins!");
            gameEnded = true;
            CancelInvoke();

            BattleReward rewardSystem = FindObjectOfType<BattleReward>();
            RewardUI rewardUI = FindObjectOfType<RewardUI>();

            if (rewardSystem != null && rewardUI != null)
            {
                var rewards = rewardSystem.GenerateRewardChoices(PlayerState.Instance.playerData.actProgress);
                rewardUI.ShowRewards(rewards);
            }
            else
            {
                Debug.LogError("Reward system or UI not found.");
            }
        }
    }

    public int ResolveBladedShadowDamage(List<int> diceValues)
    {
        if (diceValues == null || diceValues.Count == 0) return 0;

        int baseDamage = diceValues.Sum();
        int finalDamage = baseDamage;

        Dictionary<int, int> valueCounts = new();
        foreach (int value in diceValues)
        {
            if (!valueCounts.ContainsKey(value))
                valueCounts[value] = 1;
            else
                valueCounts[value]++;
        }

        if (valueCounts.Values.Any(count => count == 3))
        {
            finalDamage *= 3;
            Debug.Log("💥 Triples detected! Tripling damage.");
        }
        else if (valueCounts.Values.Any(count => count == 2))
        {
            finalDamage *= 2;
            Debug.Log("🔥 Doubles detected! Doubling damage.");
        }

        Debug.Log($"[Bladed Shadow] Final Damage: {finalDamage}");
        return finalDamage;
    }

    public bool IsPlayerTurn() => currentTurn == Turn.Player;
    public bool HasRolledYet() => playerHasRolled;

    public void EndPlayerTurn()
    {
        if (currentTurn == Turn.Player && !gameEnded)
        {
            Invoke(nameof(SwitchToEnemyTurn), 2f);
            currentTurn = Turn.Enemy;
        }
    }
}
