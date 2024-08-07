using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject hpBarPrefab;

    [SerializeField] private Transform barPosition;

    [SerializeField] private float firstHp= 10f;
    [SerializeField] private float maxHp = 10f;

    public float CurrentHp { get; set; }

    private Image _hpBar;
    private Enemy _enemy;

    void Start()
    {
        CreateBar();
        CurrentHp = firstHp;

        _enemy = GetComponent<Enemy>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f); 
        }

        _hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount,CurrentHp / maxHp , Time.deltaTime * 10f);
    }

    private void CreateBar()
    {
        GameObject newBar = Instantiate(hpBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHpContainer container = newBar.GetComponent<EnemyHpContainer>();
        _hpBar = container.DamageHpImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHp -= damageReceived;
        if (CurrentHp <= 0) //eger can sifirlanirsa die fonksiyonunu cagir.
        {
            CurrentHp = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    public void ResetHp() //oyunu bitiren dusman geri dondugunde cani tazelemek icin, sonradan kalkabilir.
    {
        CurrentHp = firstHp;
        _hpBar.fillAmount = 1f;
    }

    private void Die()
    {
        SuccesManager.Instance.AddAdvance("Kill20", 1);
        SuccesManager.Instance.AddAdvance("Kill50", 1);
        SuccesManager.Instance.AddAdvance("Kill100", 1);
        //basarilari algilamasini saglar.
        OnEnemyKilled?.Invoke(_enemy);
        //can bittiginde olen dusmani tekrar kapsule yollar.
    }
}
