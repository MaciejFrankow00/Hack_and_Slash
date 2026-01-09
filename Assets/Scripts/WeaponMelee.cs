using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    private Animator animator;
    [SerializeField] float damage;

    private Statistics _statistics;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    
    private void Start()
    {
        _statistics = StatisticsProvider.Instance.GetStatistics();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("MeleeAttack");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        HealthController healthController = other.GetComponent<HealthController>();
        if(healthController == null) return;
        
        float finalDmg = damage + (damage * _statistics.StatPoints[BaseStatType.STRENGTH] * 0.1f);
        healthController.TakeDamage(finalDmg);
    }
}
