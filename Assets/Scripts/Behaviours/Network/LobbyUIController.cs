using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class LobbyUIController : NetworkBehaviour
    {
        [SerializeField] GameObject playerLinePrefab;
        [SerializeField] GameObject roomList;

        public void CloseGame()
        {
            if (isServer)
            {
                NetworkManagerCustom.Instance.StopHost();
            }
            else
            {
                NetworkManagerCustom.Instance.StopClient();
            }   
        }

        internal void AddPlayerLine(LobbyPlayer lobbyPlayer)
        {
            GameObject playerLine = Instantiate(playerLinePrefab);
            playerLine.transform.SetParent(roomList.transform);
            playerLine.transform.localScale = Vector3.one;
            playerLine.GetComponentInChildren<Text>().text = lobbyPlayer.nickname;
        }

        [Command]
        public void SetLobbyPlayerUIObject(GameObject uiObject, LobbyPlayer lobbyPlayer)
        {
            lobbyPlayer.uiObject = uiObject;
        }
    }
}
