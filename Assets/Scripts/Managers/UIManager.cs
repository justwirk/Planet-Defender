using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singular<UIManager>
{

    [Header("Boards")]
    [SerializeField] private GameObject weaponShopBoard;
    [SerializeField] private GameObject seatUIBoard;
    [SerializeField] private GameObject successBoard;
    [SerializeField] private GameObject gameOverBoard;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI weaponLevelText;
    [SerializeField] private TextMeshProUGUI allCoinText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverAllCoinText;


    private Seat _currentSeatSelected;

    private void Update()
    {
        allCoinText.text = CoinSystem.Instance.AllCoins.ToString();
        hpText.text = LevelManager.Instance.AlLives.ToString();
        currentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave}";
    }

    public void ShowTheEndBoard()
    {
        gameOverBoard.SetActive(true);
        Time.timeScale = 0;
        gameOverAllCoinText.text = CoinSystem.Instance.AllCoins.ToString();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void OpenSuccessBoard(bool cases)
    {
        successBoard.SetActive(cases);
    }

    public void CloseShopBoard()
    {
        weaponShopBoard.SetActive(false);
    }

    public void CloseSeatUIBoard()
    {
        _currentSeatSelected.CloseAttackCarrySprite();
        seatUIBoard.SetActive(false);
    }
    

    public void UpgradeWeapon()
    {
        _currentSeatSelected.Weapon.WeaponUpgrade.UpgradeWeapon();
        UpdateUpgradeText();
        UpdateWeaponLevel();
        UpdateSellPrice();
    }

    public void SellWeapon()
    {
        _currentSeatSelected.SellWeapon();
        _currentSeatSelected = null;
        seatUIBoard.SetActive(false);
    }

    private void ShowSeatUI()
    {
        seatUIBoard.SetActive(true);
        UpdateUpgradeText();
        UpdateWeaponLevel();
        UpdateSellPrice();
        
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentSeatSelected.Weapon.WeaponUpgrade.UpgradePrice.ToString();
    }

    private void UpdateWeaponLevel()
    {
        weaponLevelText.text = $"Level {_currentSeatSelected.Weapon.WeaponUpgrade.WeaponLevel}";
    }

    private void UpdateSellPrice()
    {
        int sellSum = _currentSeatSelected.Weapon.WeaponUpgrade.SellPrice();
        sellText.text = sellSum.ToString();
    }

    private void SeatSelected(Seat seatSelected)
    {
        _currentSeatSelected = seatSelected;
        if (_currentSeatSelected.IsEmpty())
        {
            weaponShopBoard.SetActive(true);
        }
        else
        {
            ShowSeatUI();
        }
    }

    private void OnEnable()
    {
        Seat.OnSeatSelected += SeatSelected;
    }

    private void OnDisable()
    {
        Seat.OnSeatSelected -= SeatSelected;
    }
}
