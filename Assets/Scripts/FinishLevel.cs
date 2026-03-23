using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public int completedLevel;

    public void Finish()
    {
        PlayerPrefs.SetInt("CurrentLevel", completedLevel);
        PlayerPrefs.SetInt("Level" + completedLevel + "Complete", 1);

        SceneManager.LoadScene("WorldOverview");
    }
}