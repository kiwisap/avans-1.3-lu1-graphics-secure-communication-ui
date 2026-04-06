using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    public Image levelImage;

    public void CompleteLevel()
    {
        Color unlocked = new Color(0.145f, 0.388f, 0.922f);
        levelImage.color = unlocked;
    }
}