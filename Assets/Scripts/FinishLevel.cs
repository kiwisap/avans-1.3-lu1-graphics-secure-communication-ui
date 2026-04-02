using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public int completedLevel;

    public void Finish()
    {
        Debug.Log("Finish called");
        Debug.Log("CompletedLevel = " + completedLevel);

        PlayerPrefs.SetInt("CurrentLevel", completedLevel);
        PlayerPrefs.SetInt("Level" + completedLevel + "Complete", 1);

        PlayerPrefs.Save();

        Debug.Log("Saved key = Level" + completedLevel + "Complete");
        Debug.Log("Saved value = " + PlayerPrefs.GetInt("Level" + completedLevel + "Complete"));

        SceneManager.LoadScene("WorldOverview");
    }
}