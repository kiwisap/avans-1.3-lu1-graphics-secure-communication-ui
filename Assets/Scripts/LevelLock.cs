using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public int requiredLevel;
    public Button button;

    private void OnEnable()
    {
        AccountManager.OnLoginSuccess += HandleLoginSuccess;
    }

    private void OnDisable()
    {
        AccountManager.OnLoginSuccess -= HandleLoginSuccess;
    }

    void Start()
    {
        UpdateInteractableState();
    }

    private void UpdateInteractableState()
    {
        // Ouders hebben altijd toegang tot alle levels
        if (GameState.IsOuder) return;

        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        if (currentLevel < requiredLevel - 1)
        {
            button.interactable = false;
        }
    }

    private void HandleLoginSuccess()
    {
        UpdateInteractableState();
    }
}