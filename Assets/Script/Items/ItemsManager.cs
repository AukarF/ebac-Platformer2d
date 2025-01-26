using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;

    public int coins;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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
