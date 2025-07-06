using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardUI : MonoBehaviour
{
    public GameObject rewardPanel;  // Assign your RewardPanel
    public Button goldButton;
    public Button diceButton;
    public Button upgradeButton;

    private BattleReward rewardSystem;
    private List<Reward> currentRewards;

    void Start()
    {
        rewardSystem = FindObjectOfType<BattleReward>();
        rewardPanel.SetActive(false);
    }

    public void ShowRewards(List<Reward> rewards)
    {
        if (rewards == null || rewards.Count < 3)
        {
            Debug.LogError("Not enough rewards provided.");
            return;
        }

        currentRewards = rewards;
        rewardPanel.SetActive(true);

        goldButton.GetComponentInChildren<TextMeshProUGUI>().text = rewards[0].GetDescription();
        diceButton.GetComponentInChildren<TextMeshProUGUI>().text = rewards[1].GetDescription();
        upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = rewards[2].GetDescription();

        goldButton.onClick.RemoveAllListeners();
        goldButton.onClick.AddListener(() => SelectReward(0));

        diceButton.onClick.RemoveAllListeners();
        diceButton.onClick.AddListener(() => SelectReward(1));

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => SelectReward(2));
    }

    private void SelectReward(int index)
    {
        rewardSystem.ApplyReward(currentRewards[index]);
        rewardPanel.SetActive(false);
        // Optionally load map scene or enable continue button here
    }
}
