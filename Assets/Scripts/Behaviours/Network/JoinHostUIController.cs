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
            NetworkManagerCustom.Instance.OnError += ShowMessage;
            errorMessage.SetActive(false);
            NetworkManagerCustom.Instance.SetNickname(nickNameInput.text);
            NetworkManagerCustom.Instance.networkAddress = ipAdressInput.text;
            NetworkManagerCustom.Instance.StartClient();
        }

        private void ShowMessage()
        {
            errorMessage.SetActive(true);
            NetworkManagerCustom.Instance.OnError -= ShowMessage;
        }

        public void HostGame()
        {
            NetworkManagerCustom.Instance.SetNickname("The Host");
            NetworkManagerCustom.Instance.StartHost();
        }
    }

}
