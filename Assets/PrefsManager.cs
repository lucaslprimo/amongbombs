using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    private const string NICKNAME_KEY = "nickname";

    public static PrefsManager Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SaveNickname(string nickname)
    {
        PlayerPrefs.SetString(NICKNAME_KEY, nickname);
    }

    public string GetNickname()
    {
        return PlayerPrefs.GetString(NICKNAME_KEY);
    }
}
