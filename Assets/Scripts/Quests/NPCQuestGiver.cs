using UnityEngine;

public class NPCQuestGiver : Interactable
{
    public bool questStarted = false;
    public bool questCompleted = false;

    public override void Interact()
    {
        if (!questStarted)
        {
            QuestManager.instance.StartQuest();
            questStarted = true;
            Debug.Log("Quest rozpoczêty!");
        }
        else if (QuestManager.instance.quest.isCompleted && !questCompleted)
        {
            QuestManager.instance.CompleteQuest();
            questCompleted = true;
            Debug.Log("Quest zakoñczony!");
        }
    }
}
