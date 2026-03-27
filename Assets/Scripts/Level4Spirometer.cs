using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level4Spirometer : MonoBehaviour
{
    public RectTransform ball1;
    public RectTransform ball2;
    public RectTransform ball3;

    public TMP_Text missionText;
    public Button finishButton;

    public AudioSource audioSource;
    public AudioClip rise1;
    public AudioClip rise2;
    public AudioClip rise3;
    public AudioClip applause;

    public GameObject confetti;

    private AudioClip micClip;

    private bool sound1Played = false;
    private bool sound2Played = false;
    private bool sound3Played = false;

    void Start()
    {
        finishButton.gameObject.SetActive(false);
        confetti.SetActive(false);

        micClip = Microphone.Start(null, true, 10, 44100);

        missionText.text = "Blaas zo hard mogelijk!";
    }

    void Update()
    {
        float volume = GetMicVolume();

        if (ball1.anchoredPosition.y < 250)
        {
            MoveBall(ball1, Mathf.Clamp(volume * 12000f, 0, 500));

            if (volume > 0.01f && !sound1Played)
            {
                audioSource.PlayOneShot(rise1);
                sound1Played = true;
            }
        }
        else if (ball2.anchoredPosition.y < 250)
        {
            MoveBall(ball2, Mathf.Clamp(volume * 12000f, 0, 500));

            if (volume > 0.03f && !sound2Played)
            {
                audioSource.PlayOneShot(rise2);
                sound2Played = true;
            }
        }
        else if (ball3.anchoredPosition.y < 250)
        {
            MoveBall(ball3, Mathf.Clamp(volume * 13000f, 0, 500));

            if (volume > 0.05f && !sound3Played)
            {
                audioSource.PlayOneShot(rise3);
                sound3Played = true;
            }
        }
        else
        {
            FinishLevel();
        }

        if (volume < 0.01f)
        {
            sound1Played = false;
            sound2Played = false;
            sound3Played = false;
        }
    }

    void MoveBall(RectTransform ball, float targetY)
    {
        Vector2 pos = ball.anchoredPosition;
        pos.y = Mathf.MoveTowards(pos.y, -250 + targetY, 400f * Time.deltaTime);
        ball.anchoredPosition = pos;
    }

    float GetMicVolume()
    {
        int micPosition = Microphone.GetPosition(null) - 128;

        if (micPosition < 0) return 0;

        float[] samples = new float[128];
        micClip.GetData(samples, micPosition);

        float level = 0;

        foreach (float sample in samples)
        {
            level += Mathf.Abs(sample);
        }

        return level / 128;
    }

    void FinishLevel()
    {
        missionText.text = "Perfect gedaan!";

        finishButton.gameObject.SetActive(true);

        audioSource.PlayOneShot(applause);

        confetti.SetActive(true);
    }
}