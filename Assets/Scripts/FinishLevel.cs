using Assets.Scripts.Models;
using Assets.Scripts.Models.Dto;
using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public int completedLevel;

    private AccountService accountService;

    void Start()
    {
        // Find or create AccountService
        accountService = FindAnyObjectByType<AccountService>();
        if (accountService == null)
        {
            GameObject accountGO = new GameObject("AccountService");
            accountService = accountGO.AddComponent<AccountService>();
        }
    }

    public async void Finish()
    {
        Debug.Log($"Level completed, updating level... ({completedLevel})");

        // If no access token -> user not logged in, update level locally only
        if (string.IsNullOrWhiteSpace(PlayerPrefs.GetString("AccessToken")))
        {
            UpdateCurrentLevelLocally();
            Debug.Log("Level updated locally successfully");
            return;
        }

        // Otherwise, update level on server and then locally
        ApiResult<UserDto> result = await accountService.UpdateCurrentLevelAsync(completedLevel);
        if (result.Ok)
        {
            UpdateCurrentLevelLocally();
            Debug.Log("Level updated successfully");

            SceneManager.LoadScene("WorldOverview");
        }
    }

    private void UpdateCurrentLevelLocally()
    {
        PlayerPrefs.SetInt("CurrentLevel", completedLevel);
        PlayerPrefs.Save();
    }
}