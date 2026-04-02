using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    public Image levelImage;

    public void CompleteLevel()
    {
        levelImage.color = Color.green;
    }
}