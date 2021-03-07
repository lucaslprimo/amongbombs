using Mirror;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class LobbyPlayer : NetworkBehaviour
    {
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

        void Start()
        {
            lobbyUIController = FindObjectOfType<LobbyUIController>();
            lobbyUIController.AddPlayerLine(this);
        }
    }
}
