using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;

public class ItemsManager : Singleton<ItemsManager>
{

    public int coins;


    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
    }
}
