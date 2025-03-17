using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class PrefsManager
    {
        private const string NICKNAME_KEY = "nickname";

        public static void SaveNickname(string nickname)
        {
            PlayerPrefs.SetString(NICKNAME_KEY, nickname);
            PlayerPrefs.Save();
        }

        public static string GetNickname()
        {
            return PlayerPrefs.GetString(NICKNAME_KEY);
        }
    }
}
