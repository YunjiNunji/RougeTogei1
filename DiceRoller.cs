using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private List<DiceRollNew> diceToRoll = new();

    void Start()
    {
        // Automatically find all dice in the scene on startup
        diceToRoll = new List<DiceRollNew>(FindObjectsOfType<DiceRollNew>());
    }

    public void RollAllDice()
    {
        Player player = FindObjectOfType<Player>();
        bool isHighroller = player.characterName == "Highroller";

        DiceStatsLogger logger = FindObjectOfType<DiceStatsLogger>();

        foreach (var die in diceToRoll)
        {
            if (die != null)
            {
                int result = isHighroller
                    ? DiceUtility.RollHighroller(6)
                    : DiceUtility.RollStandard(6);

                die.SetRolledValue(result);

                if (logger != null)
                    logger.RecordRoll(result);
            }
        }
    }
}
