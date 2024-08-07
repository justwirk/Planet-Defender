using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public static Action<Seat> OnSeatSelected;
    public static Action OnWeaponSold;

    [SerializeField] private GameObject attackCarrySprite;

    public Weapon Weapon { get; set; }

    private float _carrySize;
    private Vector3 _carryNaturalSize;

    private void Start()
    {
        _carrySize = attackCarrySprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _carryNaturalSize = attackCarrySprite.transform.localScale;
    }

    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    public bool IsEmpty()
    {
        return Weapon == null;
    }

    public void CloseAttackCarrySprite()
    {
        attackCarrySprite.SetActive(false);
    }

    public void SelectWeapon()
    {
        OnSeatSelected?.Invoke(this);
        if (!IsEmpty())
        {
            ShowWeaponInfo();
        }
    }

    public void SellWeapon()
    {
        if (!IsEmpty())
        {
            CoinSystem.Instance.EarnCoins(Weapon.WeaponUpgrade.SellPrice());
            Destroy(Weapon.gameObject);
            Weapon = null;
            attackCarrySprite.SetActive(false);//silah satildiginda menzili kapatir
            OnWeaponSold?.Invoke();
        }
    }

    private void ShowWeaponInfo()
    {
        attackCarrySprite.SetActive(true);
        attackCarrySprite.transform.localScale = _carryNaturalSize * Weapon.AttackCarry / (_carrySize / 2);
    }
}
