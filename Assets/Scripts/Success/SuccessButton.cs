using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SuccessButton : MonoBehaviour
{
    [SerializeField] private Image successImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI advance;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Button buttonReward;

    public Success SuccessLoaded { get; set; }

    public void SettingSucces(Success success)
    {
        SuccessLoaded = success;
        successImage.sprite = success.Sprite;
        title.text = success.Title;
        advance.text = success.GetAdvance();
        reward.text = success.GoldPrize.ToString();

    }

    public void GetPrize()
    {
        if (SuccessLoaded.IsOpen)
        {
            CoinSystem.Instance.EarnCoins(SuccessLoaded.GoldPrize);
            buttonReward.gameObject.SetActive(false);
        }
    }

    private void LoadSuccessAdvance()
    {
        if (SuccessLoaded.IsOpen)
        {
            advance.text = SuccessLoaded.GetAdvanceFinished();
        }
        else
        {
            advance.text = SuccessLoaded.GetAdvance();
        }
    }

    private void InspectRewardButtonCase()
    {
        if (SuccessLoaded.IsOpen)
        {
            buttonReward.interactable = true;
        }
        else
        {
            buttonReward.interactable = false;
        }
    }

    private void UpdateAdvance(Success successWithAdvance)
    {
        if (SuccessLoaded == successWithAdvance)
        {
            LoadSuccessAdvance();
        }
    }

    private void SuccessOpen(Success success)
    {
        if (SuccessLoaded == success)
        {
            InspectRewardButtonCase();
        }
    }

    private void OnEnable()
    {
        InspectRewardButtonCase();
        LoadSuccessAdvance();
        SuccesManager.AtAdvanceUpdated += UpdateAdvance;
        SuccesManager.AtSuccessOpen += SuccessOpen;
    }

    private void OnDisable()
    {
        SuccesManager.AtAdvanceUpdated -= UpdateAdvance;
        SuccesManager.AtSuccessOpen -= SuccessOpen;
    }
}
