using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ValueChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueLabel;
    [SerializeField] private TextMeshProUGUI label;

    [SerializeField] private Button decrementValueButton;
    [SerializeField] private Button incrementValueButton;

    [field: SerializeField] public int Value {get; private set;}

    private Vector2Int _minMaxRange = new Vector2Int (0, 9);

    public UnityEvent<int, int> ValueChanged;

    public void Init(Vector2Int minMaxRange, int value, string title = "")
    {
        if(title != "") label.text = title;
        _minMaxRange = minMaxRange;
        Value = Mathf.Clamp(value, _minMaxRange.x, _minMaxRange.y);

        RefreshUI();
    }

    private void Start()
    {
        incrementValueButton.onClick.AddListener(IncrementValue);
        decrementValueButton.onClick.AddListener(DecrementValue);

        RefreshUI();
    }

    private void OnDestroy()
    {
        incrementValueButton.onClick.RemoveAllListeners();
        decrementValueButton.onClick.RemoveAllListeners();
    }

    private void IncrementValue()
    {
        Value++;

        RefreshUI();
        ValueChanged?.Invoke(Value, 1);
    }

    private void DecrementValue()
    {
        Value--;

        RefreshUI();
        ValueChanged?.Invoke(Value, -1);
    }

    private void RefreshUI()
    {
        valueLabel.text = Value.ToString();
        incrementValueButton.interactable = Value < _minMaxRange.y;
        decrementValueButton.interactable = Value > _minMaxRange.x;
    }
}
