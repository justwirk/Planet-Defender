using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float moveSpeed = 3f;

    public float MoveSpeed { get; set; }

    public TransferPoint TransferPoint { get; set; }

    public EnemyHealth EnemyHealth { get; set; }

    public Vector3 CurrentPointPosition => TransferPoint.GetTransferPointPosition(_currentTransferPointIndex);

    private int _currentTransferPointIndex;

    private Vector3 _lastPosition;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<EnemyHealth>();

        _currentTransferPointIndex = 0;
        MoveSpeed = moveSpeed;

        _lastPosition = transform.position;
    }

    private void Update()
    {
        Move();
        Turn();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopAct()
    {
        MoveSpeed = 0f;
    }

    public void ContinueAct()
    {
        MoveSpeed = moveSpeed;
    }

    private void Turn()
    {
        if (CurrentPointPosition.x > _lastPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPosition = (transform.position - CurrentPointPosition).magnitude;

        if (distanceToNextPosition < 0.1f)
        {
            _lastPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastTransferPointIndex = TransferPoint.Points.Length - 1;
        if (_currentTransferPointIndex < lastTransferPointIndex)
        {
            _currentTransferPointIndex++;
        }
        else
        {
            ReturnEnemyToCapsule();
        }
    }

    private void ReturnEnemyToCapsule()
    {
        if (OnEndReached != null)
        {
            OnEndReached.Invoke(this);
        }
        _enemyHealth.ResetHp();
        ObjectCapsule.ReturnToCapsule(gameObject);
    }

    public void AgainEnemy()
    {
        _currentTransferPointIndex = 0;
    }
}
