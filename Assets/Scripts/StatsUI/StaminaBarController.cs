using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image valueFill;

    private Statistics _statistics;

    private void Start()
    {
        _statistics = StatisticsProvider.Instance.GetStatistics();
    }

    private void Update()
    {
        label.text = $"Stamina {(int)_statistics.currentStamina}/{_statistics.baseMaxStamina}";
        valueFill.fillAmount = _statistics.currentStamina/_statistics.baseMaxStamina;
    }
}
