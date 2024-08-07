using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackCarry = 3f;

    public Enemy CurrentEnemyTarget { get; set; }
    public WeaponUpgrade WeaponUpgrade { get; set; }
    public float AttackCarry => attackCarry;


    private bool _gameStarted;
    private List<Enemy> _enemies;

    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();

        WeaponUpgrade = GetComponent<WeaponUpgrade>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateToEnemy();
    }

    private void GetCurrentEnemyTarget()
        //düşman listesindeki ilk düşmanı hedef olarak belirler. Liste boşsa, hedefi null yapar.
    {
        if (_enemies.Count <= 0 )
        {
            CurrentEnemyTarget = null;
            return;
        }
        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateToEnemy()
        //silahı hedeflenen düşmana doğru döndürür.Eğer hedeflenen düşman yoksa, dönüş işlemi yapılmaz.
    {
        if (CurrentEnemyTarget ==  null)
        {
            return;
        }
        Vector3 enemyPosition = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPosition, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }


    private void OnTriggerEnter2D(Collider2D collision)//silahin alanina giris yapmasini algilar.
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy newEnemy = collision.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
        //silahın menzilinden bir düşman çıktığında çalışır ve düşmanı listeden çıkarır.
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }

    private void OnDrawGizmos()
    //oyun nesnesi seçildiğinde silahın menzilini görsel olarak gösterir.
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackCarry;
        }
        Gizmos.DrawWireSphere(transform.position, attackCarry);
    }
} 
