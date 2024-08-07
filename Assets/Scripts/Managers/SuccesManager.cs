using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccesManager : Singular<SuccesManager>
{
    public static Action<Success> AtSuccessOpen;
    public static Action<Success> AtAdvanceUpdated;

    [SerializeField] private SuccessButton successButtonPrefab;
    [SerializeField] private Transform succesDashboard;
    [SerializeField] private Success[] successes;

    private void Start()
    {
        LoadSucces();
    }

    private void LoadSucces()
    {
        for (int i = 0; i < successes.Length; i++)
        {
            SuccessButton card = Instantiate(successButtonPrefab, succesDashboard);
            card.SettingSucces(successes[i]);
        }
    }

    public void AddAdvance(string successID, int sum)
    {
        Success successNecessary = SuccessOut(successID);
        if (successNecessary != null)
        {
            successNecessary.AddAdvance(sum);
        }
    }

    private Success SuccessOut(string successID)
    {
        for (int i = 0; i < successes.Length; i++)
        {
            if (successes[i].ID == successID)
            {
                return successes[i];
            }
        }
        return null;
    }
}
