using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Primozov.AmongBombs.Behaviours.Network;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public static PlayerNetwork Instance;

    void Start()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    [Command]
    public void CmdDisableItemOnClients(PowerUpItem item)
    {
        DisablePowerUpItemForAllClients(item);
    }

    [ClientRpc]
    public void DisablePowerUpItemForAllClients(PowerUpItem item)
    {
        item.PlayPickUpSound();
        item.spriteRenderer.enabled = false;
        item.mCollider.enabled = false;
    }

    [Command]
    public void CmdDestroyItem(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }
}
