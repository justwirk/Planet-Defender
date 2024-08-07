using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeaponBullets : WeaponBullets
{
    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_weapon.CurrentEnemyTarget != null && _weapon.CurrentEnemyTarget.EnemyHealth.CurrentHp > 0 )
            {
                FireBullet(_weapon.CurrentEnemyTarget);
            }



            _nextAttackTime = Time.time + delayFiilBullet;
        }
    }
    protected override void FiilBullet()  { }

    private void FireBullet(Enemy enemy)
    {
        GameObject model = _capsule.GetInstanceFromCapsule();
        model.transform.position = bulletSpawnPosition.position;

        Bullet bullet = model.GetComponent<Bullet>();
        _currentBulletLoaded = bullet;
        _currentBulletLoaded.WeaponOwner = this;
        _currentBulletLoaded.ResetBullet();
        _currentBulletLoaded.SetEnemy(enemy);
        _currentBulletLoaded.Damage = Damage;
        model.SetActive(true);


    }
}
