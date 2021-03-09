using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using static Mirror.NetworkRoomManager;
using System.Collections.Generic;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class LobbyUIController : NetworkBehaviour
    {
        private const string READY = "Ready";
        private const string NOT_READY = "Not Ready";

        public enum PlayerSkin
        {
            Ball,
            Square
        }

        public struct PlayerInfo
        {
            public string nickname;
            public PlayerSkin playerSkin;
        }

        public static LobbyUIController Instance;
        [SerializeField] GameObject playerLinePrefab;
        [SerializeField] GameObject roomList;
        [SerializeField] GameObject startGameButton;
        [SerializeField] GameObject readyButton;

        private void Start()
        {
            Instance = this;

            if (LobbyPlayer.localPlayer && !LobbyPlayer.localPlayer.isServer)
            {
                startGameButton.SetActive(false);
            }
        }

        public void CloseGame()
        {
            if (isServer)
            {
                NetworkRoomManagerCustom.Instance.StopHost();
            }
            else
            {
                NetworkRoomManagerCustom.Instance.StopClient();
            }   
        }

        internal void AddPlayerLine(LobbyPlayer lobbyPlayer)
        {
            GameObject playerLine = Instantiate(playerLinePrefab);
            playerLine.transform.SetParent(roomList.transform);
            playerLine.transform.localScale = Vector3.one;
            playerLine.GetComponentsInChildren<Text>()[0].text = lobbyPlayer.playerInfo.nickname;
            playerLine.GetComponentsInChildren<Text>()[1].text = lobbyPlayer.readyToBegin ? READY : NOT_READY;
        }

        public void RefreshUIList(List<NetworkRoomPlayer> list)
        {
            ClearUIList();
            foreach (NetworkRoomPlayer roomSlot in list)
            {
                AddPlayerLine(roomSlot as LobbyPlayer);
            }
        }

        public void SetReady()
        {
            LobbyPlayer.localPlayer.CmdChangeReadyState(!LobbyPlayer.localPlayer.readyToBegin);
        }

        private void ClearUIList()
        {
            if (roomList != null)
            {
                foreach (Transform obj in roomList.transform)
                {
                    Destroy(obj.gameObject);
                }
            }
           
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}
