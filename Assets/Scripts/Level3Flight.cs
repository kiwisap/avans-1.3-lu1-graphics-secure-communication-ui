using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level3Flight : MonoBehaviour
{
    public BackgroundScroller scroller;
    public RectTransform goatBalloon;
    public Image goatImage;

    public Sprite flyingSprite;
    public Sprite stopSprite;

    public TextMeshProUGUI heightText;
    public GameObject finishButton;

    private AudioClip micClip;
    private string micName;

    private bool hasStarted = false;
    private float silenceTimer = 0f;
    private float height = 0f;

    void Start()
    {
        micName = Microphone.devices[0];
        micClip = Microphone.Start(micName, true, 10, 44100);
    }

    void Update()
    {
        float volume = GetMicVolume();

        if (volume > 0.01f)
        {
            hasStarted = true;
            silenceTimer = 0f;

            goatImage.sprite = flyingSprite;

            goatBalloon.anchoredPosition = new Vector2(
                goatBalloon.anchoredPosition.x,
                50 + Mathf.Sin(Time.time * 8f) * 10f
            );

            height += volume * 15f;

            heightText.text = "Hoogte: " + Mathf.RoundToInt(height) + "m";

            scroller.scrollSpeed = volume * 2000f;
        }
        else if (hasStarted)
        {
            goatImage.sprite = stopSprite;

            scroller.scrollSpeed = 0f;

            silenceTimer += Time.deltaTime;

            if (silenceTimer > 0.7f)
            {
                EndLevel();
            }
        }
    }

    float GetMicVolume()
    {
        float[] data = new float[128];
        int micPos = Microphone.GetPosition(micName) - 128;

        if (micPos < 0) return 0;

        micClip.GetData(data, micPos);

        float level = 0;

        foreach (float sample in data)
        {
            level += Mathf.Abs(sample);
        }

        return level / 128;
    }

    void EndLevel()
    {
        enabled = false;
        finishButton.SetActive(true);
    }
}