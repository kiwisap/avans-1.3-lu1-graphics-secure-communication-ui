using UnityEngine;
using UnityEngine.UI;

public class WorldOverviewManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;

    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Level2Complete"));
        // check welke levels voltooid zijn
        if (PlayerPrefs.GetInt("Level1Complete") == 1)
        {
            level1Button.image.color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level2Complete") == 1)
        {
            level2Button.image.color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level3Complete") == 1)
        {
            level3Button.image.color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level4Complete") == 1)
        {
            level4Button.image.color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level5Complete") == 1)
        {
            level5Button.image.color = Color.green;
        }
    }
}