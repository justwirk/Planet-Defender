using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : Singular<CoinSystem>
{

    [SerializeField] private int coinTest;
    private string COINS_SAVE_KEY = "MYGAME_COINS";

    public int AllCoins { get; set; }

    private void Start()
    {
        PlayerPrefs.DeleteKey(COINS_SAVE_KEY);
        LoadCoins();
    }

    private void LoadCoins()
    {
        AllCoins = PlayerPrefs.GetInt(COINS_SAVE_KEY, coinTest);
    }

    public void EarnCoins(int sum)
    {
        AllCoins += sum;
        PlayerPrefs.SetInt(COINS_SAVE_KEY, AllCoins);
        PlayerPrefs.Save();
    }

    public void SpendCoins(int sum)
    {
        if (AllCoins >= sum)
        {
            AllCoins -= sum;
            PlayerPrefs.SetInt(COINS_SAVE_KEY, AllCoins);
            PlayerPrefs.Save();

        }
    }

    private void EarnCoins(Enemy enemy)
    {
        EarnCoins(10);
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += EarnCoins;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= EarnCoins;
    }
}
