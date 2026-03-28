using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMedicationManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject startPanel;
    public GameObject timerTextObject;
    public TextMeshProUGUI timerDisplay;
    public GameObject finishButton;

    [Header("Gameplay")]
    public GoatGlider goatGlider;
    public MedicationBGScroller[] backgroundScrollers;
    public float gameDuration = 60f;

    private float timeRemaining;
    private bool gameRunning = false;

    void Start()
    {
        startPanel.SetActive(true);
        timerTextObject.SetActive(false);
        finishButton.SetActive(false);
        goatGlider.StopGame();
    }

    public void OnMedicationConfirmed()
    {
        startPanel.SetActive(false);
        timerTextObject.SetActive(true);
        timeRemaining = gameDuration;
        gameRunning = true;
        goatGlider.StartGame();

        foreach (var scroller in backgroundScrollers)
            scroller.StartScrolling();
    }

    void Update()
    {
        if (!gameRunning) return;

        timeRemaining -= Time.deltaTime;
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerDisplay.text = $"{seconds}s";

        if (timeRemaining <= 0f)
        {
            gameRunning = false;
            OnTimerFinished();
        }
    }

    void OnTimerFinished()
    {
        goatGlider.StopGame();

        foreach (var scroller in backgroundScrollers)
            scroller.StopScrolling();

        timerTextObject.SetActive(false);
        finishButton.SetActive(true);
    }
}