using UnityEngine;

public class SkillUnlockManager : MonoBehaviour
{
    [SerializeField] private GameObject skillUnlockPanel;

    private Statistics statistics;

    private void Start()
    {
        statistics = GetComponent<StatisticsProvider>().GetStatistics();

        // DEBUG: Upewnij siê, ¿e ³apiemy event
        Debug.Log("SkillUnlockManager: Podpinam siê do LevelUp");
        statistics.LevelUp.AddListener(ShowUnlockPanel);

        skillUnlockPanel.SetActive(false);
    }

    private void ShowUnlockPanel()
    {
        Debug.Log("SkillUnlockManager: Otrzymano LevelUp, pokazujê UI");
        skillUnlockPanel.SetActive(true);
    }
}
