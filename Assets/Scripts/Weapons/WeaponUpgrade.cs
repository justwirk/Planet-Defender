using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeFirstPrice;
    [SerializeField] private int upgradePriceGrow;
    [SerializeField] private float dmgGrow;
    [SerializeField] private float delayLower;

    [Header("Sell")]
    [Range(0, 1)]
    [SerializeField] private float sellEquip;

    public float SellEquipment { get; set; }
    public int UpgradePrice { get; set; }
    public int WeaponLevel { get; set; }

    private WeaponBullets _weaponBullets;

    private void Start()
    {
        _weaponBullets = GetComponent<WeaponBullets>();
        UpgradePrice = upgradeFirstPrice;

        SellEquipment = sellEquip;
        WeaponLevel = 1;
    }

    public void UpgradeWeapon()
    {
        if (CoinSystem.Instance.AllCoins >= UpgradePrice)
        {
            _weaponBullets.Damage += dmgGrow;
            _weaponBullets.DelayPerShot -= delayLower;
            UpdateUpgrade();
        }
        
    }

    public int SellPrice()
    {
        int sellPrice = Mathf.RoundToInt(UpgradePrice * SellEquipment);
        return sellPrice;
    }
    private void UpdateUpgrade()
    {
        CoinSystem.Instance.SpendCoins(UpgradePrice);
        UpgradePrice += upgradePriceGrow;
        WeaponLevel++;
    }
}
