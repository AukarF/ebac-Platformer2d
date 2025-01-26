using UnityEngine;

public class ItemCollectableCoin : ItemColletableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemsManager.instance.AddCoins();
    }
}
