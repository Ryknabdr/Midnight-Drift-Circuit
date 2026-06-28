using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static void UnlockLevel(int level)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (level > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", level);
            PlayerPrefs.Save();
        }
    }

    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    public static void ResetSave()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.DeleteKey("TutorialShown"); // Reset tutorial juga
        PlayerPrefs.Save();
    }
}