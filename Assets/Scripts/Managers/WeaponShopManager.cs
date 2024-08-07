using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopManager : MonoBehaviour
{
    [SerializeField] private GameObject weaponButtonPrefab;
    [SerializeField] private Transform weaponDashboard;

    [Header("Weapon Organize")]
    [SerializeField] private WeaponOrganize[] weapons;

    private Seat _currentSeatSelected;

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            CreateWeaponButton(weapons[i]);
        }
    }

    private void CreateWeaponButton(WeaponOrganize weaponOrganize)
    {
        GameObject newModel = Instantiate(weaponButtonPrefab, weaponDashboard.position, Quaternion.identity);
        newModel.transform.SetParent(weaponDashboard);
        newModel.transform.localScale = Vector3.one;

        WeaponButton jrButton = newModel.GetComponent<WeaponButton>();
        jrButton.SetupWeaponButton(weaponOrganize);

    }

    private void SeatSelected(Seat seatSelected)
    {
        _currentSeatSelected = seatSelected;
    }

    private void SiteWeapon(WeaponOrganize weaponLoaded)
    {
        if (_currentSeatSelected != null)
        {
            GameObject weaponModel = Instantiate(weaponLoaded.WeaponPrefab);
            weaponModel.transform.localPosition = _currentSeatSelected.transform.position;
            weaponModel.transform.parent = _currentSeatSelected.transform;

            Weapon weaponSite = weaponModel.GetComponent<Weapon>();
            _currentSeatSelected.SetWeapon(weaponSite);
        }
    }

    private void WeaponSold()
    {
        _currentSeatSelected = null;
    }

    private void OnEnable()
    {
        Seat.OnSeatSelected += SeatSelected;
        Seat.OnWeaponSold += WeaponSold;
        WeaponButton.AtSiteWeapon += SiteWeapon;
    }

    private void OnDisable()
    {
        Seat.OnSeatSelected -= SeatSelected;
        Seat.OnWeaponSold -= WeaponSold;
        WeaponButton.AtSiteWeapon -= SiteWeapon;
    }
}
