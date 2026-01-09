using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public QuestData quest;
    public TextMeshProUGUI questUIText;

    private void Awake()
    {
        instance = this;
    }

    public void StartQuest()
    {
        quest.isActive = true;
        quest.currentKills = 0;
        quest.isCompleted = false;
        UpdateUI();
    }

    public void RegisterKill()
    {
        if (!quest.isActive || quest.isCompleted) return;

        quest.currentKills++;
        UpdateUI();

        if (quest.currentKills >= quest.killTargetCount)
        {
            quest.isCompleted = true;
            questUIText.text = "Back to NPC";
        }
    }

    public void CompleteQuest()
    {
        if (quest.isCompleted)
        {
            quest.isActive = false;
            questUIText.text = "Mission Complete!";
            StartCoroutine(ClearUITextAfterDelay(3f)); 
        }
    }

    private void UpdateUI()
    {
        if (quest.isActive && !quest.isCompleted)
            questUIText.text = $"Kill enemies: {quest.currentKills}/{quest.killTargetCount}";
    }

    private IEnumerator ClearUITextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        questUIText.text = "";
    }
}
