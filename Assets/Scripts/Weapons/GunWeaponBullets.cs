using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponBullets : WeaponBullets
{
    [SerializeField] private bool isDualGun;
    [SerializeField] private float radiateRange;

    protected override void Update()
    //her karede çalışır ve merminin ateşlenmesini kontrol eder. Mermilerin belirli bir gecikme ile ateşlenmesini sağlar.
    {
        if (Time.time > _nextAttackTime)
        {
            if (_weapon.CurrentEnemyTarget != null)
            {
                Vector3 wayToTarget = _weapon.CurrentEnemyTarget.transform.position - transform.position;
                FireBullet(wayToTarget);
            }

            

            _nextAttackTime = Time.time + delayFiilBullet;
        }
    }

    protected override void FiilBullet()
    {
        // Boş metod, alt sınıflar tarafından doldurulabilir
    }

    private void FireBullet(Vector3 way)
    {
        GameObject model = _capsule.GetInstanceFromCapsule();
        model.transform.position = bulletSpawnPosition.position;

        GunBullet bullet = model.GetComponent<GunBullet>();
        bullet.Way = way;
        bullet.Damage = Damage;

        if (isDualGun)
        {
            float randomRadiate = Random.Range(-radiateRange, radiateRange);
            Vector3 radiate = new Vector3(0f, 0f, randomRadiate);
            Quaternion radiateValue = Quaternion.Euler(radiate);
            Vector2 newWay = radiateValue * way;
            bullet.Way = newWay;

        }

        model.SetActive(true);
    }
}
