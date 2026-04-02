using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public int requiredLevel;
    public Button button;

    void Start()
    {
        // Ouders hebben altijd toegang tot alle levels
        if (GameState.IsOuder) return;

        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        if (currentLevel < requiredLevel - 1)
        {
            button.interactable = false;
        }
    }
}