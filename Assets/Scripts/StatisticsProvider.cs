using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsProvider : MonoBehaviour
{
    [SerializeField] private bool useAsSingleton;
    public static StatisticsProvider Instance;

    [SerializeField] private Statistics statistics;

    private void Awake()
    {
        if(useAsSingleton)
        {
            if(Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

        }

        statistics = Instantiate(statistics);
        statistics.Init();
    }

    public Statistics GetStatistics() => statistics;

    private void Update()
    {
        statistics.currentStamina += Time.deltaTime * statistics.baseStaminaRegen;
        statistics.currentStamina = Mathf.Clamp(statistics.currentStamina, 0f, statistics.baseMaxStamina);
    }
}
