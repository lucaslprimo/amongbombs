using Mirror;
using System;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class LobbyPlayerBkp : NetworkRoomPlayer
    {
        public static LobbyPlayerBkp localPlayer;

        LobbyUIController lobbyUIController;

        public enum PlayerSkin
        {
            Ball,
            Square
        }

        [SyncVar]
        public int playerIndex;
        [SyncVar]
        public string nickname;
        [SyncVar]
        public PlayerSkin playerSkin;
        internal GameObject uiObject;

        void Teste()
        {
            if (isLocalPlayer)
            {
                localPlayer = this;
            }
            lobbyUIController = FindObjectOfType<LobbyUIController>();
            //lobbyUIController.AddPlayerLine(this);
        }
         
        internal void QuitLobby(GameObject gameObject)
        {
            ClearUIOnClients(gameObject);
        }

        [ClientRpc]
        private void ClearUIOnClients(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}
