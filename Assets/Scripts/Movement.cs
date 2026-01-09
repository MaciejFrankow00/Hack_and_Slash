using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatisticsProvider))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashMovementSpeedMultiplier = 1.3f;
    [SerializeField] private float dashDuration = 2f;
    [SerializeField] private float dashStaminaCost = 10f;

    private bool _duringDash = false;
    private float _dashTimer = 0f;
    private Rigidbody _rb;
    private Vector2 _input;
    private Statistics _statistics;

    void Awake () 
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _statistics = GetComponent<StatisticsProvider>().GetStatistics();
        _statistics.StatPointsChanged.AddListener(RecalculateMovementSpeed);

        RecalculateMovementSpeed();
    }

    void Update()
    {
        if(!_duringDash)
        {
            _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _input = _input.normalized;
        }

        if(!_duringDash && Input.GetKeyDown(KeyCode.Space) && _statistics.currentStamina >= dashStaminaCost
        )
        {
            _duringDash = true;
            _dashTimer = 0f;
            _statistics.currentStamina -= dashStaminaCost;
        }

        if(_duringDash )
        {
            if(_dashTimer < dashDuration)
                _dashTimer += Time.deltaTime;
            else
            {
                _dashTimer = 0f;
                _duringDash = false;
            }
        }
    }   

    void FixedUpdate()
    {
        _rb.velocity = new Vector3(_input.x, 0f, _input.y) * (movementSpeed * (_duringDash ? dashMovementSpeedMultiplier : 1f) * Time.fixedDeltaTime);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            hit.point = new Vector3(hit.point.x, 1f, hit.point.z);
            transform.LookAt(worldPosition:hit.point, Vector3.up);
        }
    }

    private void RecalculateMovementSpeed()
    {
        float finalMovementSpeed = _statistics.baseMovementSpeed + (_statistics.baseMovementSpeed * _statistics.StatPoints[BaseStatType.DEXTERITY] * 0.01f);
        movementSpeed = finalMovementSpeed;
    }
}
