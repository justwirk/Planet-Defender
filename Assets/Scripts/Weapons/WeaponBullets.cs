using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullets : MonoBehaviour
{
    [SerializeField] protected Transform bulletSpawnPosition;
    [SerializeField] protected float delayFiilBullet = 2f;
    [SerializeField] protected float damage = 2f;

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    protected float _nextAttackTime;
    protected ObjectCapsule _capsule;
    protected Weapon _weapon;
    protected Bullet _currentBulletLoaded;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _capsule = GetComponent<ObjectCapsule>();

        Damage = damage;
        DelayPerShot = delayFiilBullet;
        FiilBullet();
    }

    protected virtual void Update()
    {
        
        if (IsWeaponEmpty())
        {
            FiilBullet();
        }

        if (Time.time > _nextAttackTime)
        {
            //mermilerin dusmana gitmesi 
            if (_weapon.CurrentEnemyTarget != null && _currentBulletLoaded != null
                && _weapon.CurrentEnemyTarget.EnemyHealth.CurrentHp > 0f)
            {
                _currentBulletLoaded.transform.parent = null;
                _currentBulletLoaded.SetEnemy(_weapon.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + DelayPerShot;
        }

       
    }


    protected virtual void FiilBullet() //mermi doldurma
    {
        GameObject newInstance = _capsule.GetInstanceFromCapsule();
        newInstance.transform.localPosition = bulletSpawnPosition.position;
        newInstance.transform.SetParent(bulletSpawnPosition);

        _currentBulletLoaded = newInstance.GetComponent<Bullet>();
        _currentBulletLoaded.WeaponOwner = this;
        _currentBulletLoaded.ResetBullet();
        _currentBulletLoaded.Damage = Damage;
        newInstance.SetActive(true);
    }

    public void ResetWeaponBullet()
    {
        _currentBulletLoaded = null;
    }

    private bool IsWeaponEmpty()
    {
        return _currentBulletLoaded == null;
    }
}
