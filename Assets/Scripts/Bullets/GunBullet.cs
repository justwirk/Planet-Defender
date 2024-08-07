using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet
{

    public Vector2 Way { get; set; }

    protected override void Update()
    {
        ShootBullet();
    }

    protected override void ShootBullet()
    {
        Vector2 motion = Way.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(motion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.EnemyHealth.CurrentHp > 0f)
            {
                AtEnemyStrike?.Invoke(enemy, Damage);//silahin hasarini gostericek
                enemy.EnemyHealth.DealDamage(Damage);
            }

            ObjectCapsule.ReturnToCapsule(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ObjectCapsule.ReturnToCapsuleDelay(gameObject, 5f));
    }
}
