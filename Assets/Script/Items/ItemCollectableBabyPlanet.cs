using UnityEngine;

public class ItemCollectableBabyPlanet : ItemColletableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemsManager.instance.AddBabyPlanets();
    }
}
