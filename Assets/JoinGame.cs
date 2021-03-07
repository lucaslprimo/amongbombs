using Primozov.AmongBombs;
using UnityEngine;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    [SerializeField] Text ipAdressInput;
    [SerializeField] Text nickNameInput;
    [SerializeField] GameObject errorMessage;

    private void Start()
    {
        
    }

    public void ClientConnectToIP()
    {
        NetworkManagerCustom.Instance.OnError += ShowMessage;
        errorMessage.SetActive(false);
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
        NetworkManagerCustom.Instance.StartHost();
    }




}
