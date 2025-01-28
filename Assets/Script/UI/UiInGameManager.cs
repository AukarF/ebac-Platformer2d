using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ebac.Core.Singleton;

public class UiInGameManager : Singleton<UiInGameManager>
{
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextBabyPlanets;

    public static void UptadeTextCoins(string s)
    {
       instance.uiTextCoins.text = s;
    }

    public static void UptadeTextBabyPlanets(string s)
    {
        instance.uiTextBabyPlanets.text = s;
    }
}
