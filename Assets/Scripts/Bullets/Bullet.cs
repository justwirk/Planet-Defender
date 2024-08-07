using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Action<Enemy, float> AtEnemyStrike;

    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float minDistanceToDealDamage = 0.1f;

    public WeaponBullets WeaponOwner { get; set; }
    public float Damage { get; set; }

    protected Enemy _enemyTarget;

    protected virtual void Update()
    {
        if (_enemyTarget != null)
        {
            ShootBullet();
            RotateBullet();
        }
        
    }

    //Merminin atilmasini saglar
    protected virtual void ShootBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position,
            moveSpeed * Time.deltaTime);
        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if (distanceToTarget < minDistanceToDealDamage)
        {
            AtEnemyStrike?.Invoke(_enemyTarget, Damage);
            _enemyTarget.EnemyHealth.DealDamage(Damage);
            WeaponOwner.ResetWeaponBullet();
            ObjectCapsule.ReturnToCapsule(gameObject);
        }
    }

    //Bir sonraki mermiyi aci duzenlenerek yollar
    private void RotateBullet()
    {
        Vector3 enemyPosition = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPosition, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }

    public void ResetBullet()
    {
        _enemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}
