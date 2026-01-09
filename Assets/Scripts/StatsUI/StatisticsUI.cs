using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI availableStatsLabel;
    [SerializeField] private ValueChanger valueChangerPrefab;
    [SerializeField] private Transform valueChangersParent;
    [SerializeField] private Button confirmButton;

    private Statistics _playerStats;
    private CanvasGroup _canvasGroup;

    private bool _visible;

    private Dictionary<BaseStatType, int> _savedStatPoints = new();

    private void Start()
    {
        _playerStats = StatisticsProvider.Instance.GetStatistics();
        _playerStats.LevelUp.AddListener(OneLeveledUp);
        _canvasGroup = GetComponent<CanvasGroup>();
        confirmButton.onClick.AddListener(ConfirmStats);
        
        CasheStats();
        SpawnStatValueChangers();
    }

    private void OneLeveledUp()
    {
        Refresh();
    }

    private void ConfirmStats()
    {
        CasheStats();
        _playerStats.StatPointsChanged?.Invoke();
        Refresh();
    }

    private void CasheStats()
    {
        _savedStatPoints.Clear();
        foreach (var stat in _playerStats.StatPoints)
        {
            _savedStatPoints.Add(stat.Key, stat.Value);
        }
    }

    private void SpawnStatValueChangers()
    {
        foreach (Transform child in valueChangersParent) Destroy(child.gameObject);

        foreach (var stat in _playerStats.StatPoints)
        {
            ValueChanger statValueChanger = Instantiate(valueChangerPrefab, valueChangersParent);
            statValueChanger.Init(new Vector2Int(_savedStatPoints[stat.Key], stat.Value + _playerStats.baseStatPointsToDistribute), stat.Value, stat.Key.ToString());
            statValueChanger.ValueChanged.AddListener((value, sign) => BaseStatChanged(stat.Key, value, sign));
        }
    }

    private void BaseStatChanged(BaseStatType statType, int statValue, int sign)
    {
        _playerStats.baseStatPointsToDistribute -= sign;
        _playerStats.StatPoints[statType] = statValue;
        Refresh();
    }

    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            _visible = !_visible;
            Refresh();
        }
    }

    private void Refresh()
    {
        _canvasGroup.alpha = _visible ? 1:0;
        _canvasGroup.interactable = _visible;
        _canvasGroup.blocksRaycasts = _visible;

        if(!_visible) return;

        bool canConfirm = false;
        foreach (var stat in _playerStats.StatPoints)
        {
            if(_savedStatPoints[stat.Key] != stat.Value)
            {
                canConfirm = true;
                break;
            }
        }

        confirmButton.interactable = canConfirm;
        availableStatsLabel.text = $"Skill Points: {_playerStats.baseStatPointsToDistribute}";
        SpawnStatValueChangers();
    }
}
