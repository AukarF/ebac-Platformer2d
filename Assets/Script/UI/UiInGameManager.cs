using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ebac.Core.Singleton;

public class UiInGameManager : Singleton<UiInGameManager>
{
    public TextMeshProUGUI uiTextCoins;

    public static void UptadeTextCoins(string s)
    {
       instance.uiTextCoins.text = s;
    }
}
