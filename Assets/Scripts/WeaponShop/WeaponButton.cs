using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public static Action<WeaponOrganize> AtSiteWeapon;

    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponPrice;

    public WeaponOrganize WeaponLoaded { get; set; }

    public void SetupWeaponButton(WeaponOrganize weaponOrganize)
    {
        WeaponLoaded = weaponOrganize;
        weaponImage.sprite = weaponOrganize.WeaponShopSprite;
        weaponPrice.text = weaponOrganize.WeaponShopPrice.ToString();
    }

    public void SiteWeapon()
    {
        if (CoinSystem.Instance.AllCoins >= WeaponLoaded.WeaponShopPrice)
        {
            CoinSystem.Instance.SpendCoins(WeaponLoaded.WeaponShopPrice);
            UIManager.Instance.CloseShopBoard();
            AtSiteWeapon?.Invoke(WeaponLoaded);

        }
    }
}
