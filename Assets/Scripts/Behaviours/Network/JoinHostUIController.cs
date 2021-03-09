using Primozov.AmongBombs;
using UnityEngine;
using UnityEngine.UI;


namespace Primozov.AmongBombs.Behaviours.Network
{
    public class JoinHostUIController : MonoBehaviour
    {
        [SerializeField] Text ipAdressInput;
        [SerializeField] Text nickNameInput;
        [SerializeField] GameObject errorMessage;

        public void ClientConnectToIP()
        {
            NetworkRoomManagerCustom.Instance.OnError += ShowMessage;
            errorMessage.SetActive(false);
            NetworkRoomManagerCustom.Instance.SetNickname(nickNameInput.text);
            NetworkRoomManagerCustom.Instance.networkAddress = ipAdressInput.text;
            NetworkRoomManagerCustom.Instance.StartClient();
        }

        private void ShowMessage()
        {
            if(errorMessage != null)
                errorMessage.SetActive(true);
            NetworkRoomManagerCustom.Instance.OnError -= ShowMessage;
        }

        public void HostGame()
        {
            NetworkRoomManagerCustom.Instance.SetNickname("The Host");
            NetworkRoomManagerCustom.Instance.StartHost();
        }
    }

}
