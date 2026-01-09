using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 1f;
    private Vector3 _direction;


    public void Init(Vector3 targetDirection)
    {
        _direction = targetDirection;
        transform.LookAt(transform.position + targetDirection);
    }

    public void Init(Vector3 targetDirection, float speed)
    {
        this.speed = speed;
        Init(targetDirection);
    }

    private void Update()
    {
        transform.position += _direction.normalized * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;

        HealthController healthController = other.GetComponent<HealthController>();
        if(healthController == null) return;

        healthController.TakeDamage(damage);
    }
}
