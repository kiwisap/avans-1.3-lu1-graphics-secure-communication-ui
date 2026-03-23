using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public int requiredLevel;
    public Button button;

    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        if (currentLevel < requiredLevel - 1)
        {
            button.interactable = false;
        }
    }
}