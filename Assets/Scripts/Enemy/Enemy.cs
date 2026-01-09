using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints = new();
    [SerializeField] private int currentPatrolPointIndex;
    [SerializeField] private float chaseRadius = 10f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private EnemyState _state = EnemyState.PATROL;
    private NavMeshAgent _navMeshAgent;
    private Transform _chaseTarget;
    private float _attackTimer = 0f;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckState();

        switch(_state)
        {
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.CHASE:
                Chase();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
            default:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        if(patrolPoints.Count == 0)
            return;

        _navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

        if(Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1.5f)
        {
            currentPatrolPointIndex++;
            if(currentPatrolPointIndex >= patrolPoints.Count)
            {
                currentPatrolPointIndex = 0;
            }
        }
    }

    private void Chase()
    {
        _navMeshAgent.SetDestination(_chaseTarget.position);
    }

    private void Attack()
    {
        _navMeshAgent.SetDestination(transform.position);

        _attackTimer += Time.deltaTime;
        if(_attackTimer >= 1f/attackSpeed)
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.Init(_chaseTarget.position - transform.position);
            _attackTimer = 0f;
        }
    }

    private void CheckState()
    {
        var targets = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Player"));
        if(targets.Length > 0)
        {
            _chaseTarget = targets[0].transform;
            _state = EnemyState.ATTACK;
            return;
        }

        targets = Physics.OverlapSphere(transform.position, chaseRadius, LayerMask.GetMask("Player"));
        if(targets.Length > 0)
        {
            _chaseTarget = targets[0].transform;
            _state = EnemyState.CHASE;
            return;
        }

        _state = EnemyState.PATROL;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        ReadOnlySpan<Vector3> pPoints = patrolPoints.Select(x => x.position).ToArray();
        Gizmos.DrawLineStrip(pPoints, true);

        foreach(Vector3 point in pPoints)
        {
            Gizmos.DrawSphere(point, 0.5f);
        }
    }
}


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}