using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(StatisticsProvider))]
public class HealthController : MonoBehaviour
{
    public static Action<int> OnDied;

    [SerializeField] private float currentHP;
    [SerializeField] private float maxHP;
    private TextMesh text;
    private Statistics _statistics;

    void Awake()
    {
        text = GetComponentInChildren<TextMesh>();

    }
    void Start()
    {
        _statistics = GetComponent<StatisticsProvider>().GetStatistics();
        maxHP = _statistics.baseMaxHealth;
        currentHP = maxHP;    

    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage;

        // Jeœli to gracz, uwzglêdnij redukcjê obra¿eñ
        PlayerStats playerStats = GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            finalDamage = playerStats.ApplyDamage(damage);
        }

        currentHP -= Mathf.Abs(finalDamage);
        EvaluateDeath();
    }

    public void EvaluateDeath()
    {
        if(currentHP > 0) return;        

        Destroy(gameObject);
        OnDied?.Invoke(_statistics.xpOnDeath);

        QuestManager.instance.RegisterKill();
        Destroy(gameObject);

    }

    public void Heal(float heal)
    {
        currentHP += heal;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    private void DisplayHealth()
    {
        text.text = currentHP.ToString();
    }
}
