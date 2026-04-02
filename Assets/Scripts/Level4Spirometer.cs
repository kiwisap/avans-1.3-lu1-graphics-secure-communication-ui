using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Level4Spirometer : MonoBehaviour
{
    public RectTransform ball1;
    public RectTransform ball2;
    public RectTransform ball3;

    public TMP_Text missionText;
    public TMP_Text countdownText;
    public TMP_Text restText;

    public Button startButton;
    public Button finishButton;

    public AudioSource audioSource;
    public AudioClip applause;

    public GameObject confetti;

    private AudioClip micClip;

    private int currentAttempt = 0;
    private bool isBlowing = false;
    private float silenceTimer = 0f;

    private float minY = -250f;
    private float maxY = 250f;

    void Start()
    {
        finishButton.gameObject.SetActive(false);
        confetti.SetActive(false);
        countdownText.gameObject.SetActive(false);
        restText.gameObject.SetActive(false);

        micClip = Microphone.Start(null, true, 10, 44100);

        missionText.text = "3 pogingen nodig";
    }

    void Update()
    {
        if (!isBlowing) return;

        float volume = GetMicVolume();

        float target1 = Mathf.Clamp(minY + volume * 20000f, minY, maxY);
        float target2 = Mathf.Clamp(minY + (volume - 0.015f) * 20000f, minY, maxY);
        float target3 = Mathf.Clamp(minY + (volume - 0.03f) * 20000f, minY, maxY);

        MoveBall(ball1, target1);

        if (ball1.anchoredPosition.y >= maxY - 5f)
        {
            MoveBall(ball2, target2);
        }
        else
        {
            MoveBall(ball2, minY);
        }

        if (ball2.anchoredPosition.y >= maxY - 5f)
        {
            MoveBall(ball3, target3);
        }
        else
        {
            MoveBall(ball3, minY);
        }

        if (volume < 0.01f)
        {
            silenceTimer += Time.deltaTime;

            if (silenceTimer > 1f)
            {
                EndAttempt();
            }
        }
        else
        {
            silenceTimer = 0f;
        }
    }

    public void StartAttempt()
    {
        startButton.interactable = false;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "BLAZEN!";
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);

        isBlowing = true;
    }

    void EndAttempt()
    {
        isBlowing = false;

        currentAttempt++;

        if (currentAttempt >= 3)
        {
            missionText.text = "Perfect gedaan!";
            finishButton.gameObject.SetActive(true);
            confetti.SetActive(true);

            audioSource.PlayOneShot(applause);
        }
        else
        {
            StartCoroutine(RestPhase());
        }
    }

    IEnumerator RestPhase()
    {
        restText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        ResetBalls();

        restText.gameObject.SetActive(false);

        missionText.text = "Poging " + (currentAttempt + 1);

        startButton.interactable = true;
    }

    void ResetBalls()
    {
        ball1.anchoredPosition = new Vector2(ball1.anchoredPosition.x, minY);
        ball2.anchoredPosition = new Vector2(ball2.anchoredPosition.x, minY);
        ball3.anchoredPosition = new Vector2(ball3.anchoredPosition.x, minY);
    }

    void MoveBall(RectTransform ball, float targetY)
    {
        Vector2 pos = ball.anchoredPosition;
        pos.y = Mathf.MoveTowards(pos.y, targetY, 400f * Time.deltaTime);
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
}