using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using TMPro;

public class ItemsManager : Singleton<ItemsManager>
{

    public int coins;
    public TextMeshProUGUI uiTextCoins;

    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins = 0;
        UptadeUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UptadeUI();
    }

    private void UptadeUI()
    {
        uiTextCoins.text = coins.ToString();
    }

}
