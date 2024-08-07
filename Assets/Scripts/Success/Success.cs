using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Success")]
public class Success : ScriptableObject
{
    public string ID;
    public string Title;
    public int MoveToNext;
    public int GoldPrize;
    public Sprite Sprite;


    public bool IsOpen { get; set; }

    private int CurrentAdvance;

    public void AddAdvance(int sum)
    {
        CurrentAdvance += sum;
        SuccesManager.AtAdvanceUpdated?.Invoke(this);
        InspectOpenCase();
    }

    private void InspectOpenCase()
    {
        if (CurrentAdvance >= MoveToNext)
        {
            OpenSuccess();
        }
    }

    private void OpenSuccess()
    {
        IsOpen = true; 
        SuccesManager.AtSuccessOpen?.Invoke(this);
    }

    public string GetAdvance()
    {
        return $"{CurrentAdvance}/{MoveToNext}";
    }

    public string GetAdvanceFinished()
    {
        return $"{MoveToNext}/{MoveToNext}";
    }

    public void OnEnable()
    {
        IsOpen = false;
        //her oyun basladiginda sifirla
        CurrentAdvance = 0; 
    }
}
