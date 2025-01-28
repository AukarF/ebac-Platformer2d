using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using TMPro;

public class ItemsManager : Singleton<ItemsManager>
{

    public SOInt coins;
    public TextMeshProUGUI uiTextCoins;

    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins.value = 0;
        UptadeUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UptadeUI();
    }

    private void UptadeUI()
    {
        //uiTextCoins.text = coins.ToString();
        //UiInGameManager.UptadeTextCoins(coins.value.ToString());
    }

}
