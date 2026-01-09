using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatisticsProvider))]
public class TrackXP : MonoBehaviour
{
    private Statistics _statistics;

    private void Start()
    {
        _statistics = GetComponent<StatisticsProvider>().GetStatistics();
        HealthController.OnDied += OnSomethingDied;
    }

    private void OnSomethingDied(int xp)
    {
        _statistics.AddXP(xp);
    }
    
}
