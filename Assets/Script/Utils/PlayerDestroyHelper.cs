using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDestroyHelper : MonoBehaviour
{
    public Player player;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void KillPlayer()
    {
        player.DestroyMe();
    }
}
