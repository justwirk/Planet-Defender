using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singular<LevelManager>
{
    [SerializeField] private int lives = 10;

    public int AlLives { get; set; }
    public int CurrentWave { get; set; }

    private void Start()
    {
        AlLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLives(Enemy enemy)
    {
        AlLives--;
        if (AlLives <= 0 )
        {
            AlLives = 0; //GAMEOVER BURAYA BI SEKIL CEK.
            TheEnd();
        }
    }

    private void TheEnd()
    {
        UIManager.Instance.ShowTheEndBoard();
    }

    private void OnWaveFinished()
    {
        CurrentWave++;
        SuccesManager.Instance.AddAdvance("Waves10", 1);
        SuccesManager.Instance.AddAdvance("Waves20", 1);
        SuccesManager.Instance.AddAdvance("Waves50", 1);
        SuccesManager.Instance.AddAdvance("Waves100", 1);
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        Factory.WaveFinished += OnWaveFinished;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        Factory.WaveFinished -= OnWaveFinished;
    }
}
