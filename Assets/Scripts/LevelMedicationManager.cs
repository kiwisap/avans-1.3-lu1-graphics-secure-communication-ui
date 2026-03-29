using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMedicationManager : MonoBehaviour
{
    [Header("Countdown")]
    public TextMeshProUGUI countdownDisplay;
    public float countdownDuur = 3f;
    private bool countingDown = false;
    private float countdownTimer;

    [Header("UI")]
    public GameObject startPanel;
    public GameObject timerTextObject;
    public TextMeshProUGUI timerDisplay;
    public GameObject finishButton;

    [Header("Gameplay")]
    public GoatGlider goatGlider;
    public MedicationBGScroller backgroundScroller;
    public float gameDuration = 60f;
    private float timeRemaining;
    private bool gameRunning = false;
    public ProceduralGround ground;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    void Start()
    {
        startPanel.SetActive(true);
        timerTextObject.SetActive(false);
        finishButton.SetActive(false);
        gameOverPanel.SetActive(false);
        ground.gameObject.SetActive(false); // VERBERG GROND BIJ START
        goatGlider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        goatGlider.StopGame();
    }

    public void OnMedicationConfirmed()
    {
        startPanel.SetActive(false);
        ground.gameObject.SetActive(true);
        goatGlider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        countdownDisplay.gameObject.SetActive(true);
        countdownTimer = countdownDuur;
        countingDown = true;
        backgroundScroller.StartScrolling();
        ground.StartScrolling();
    }

    void Update()
    {
        // 1. Countdown
        if (countingDown)
        {
            countdownTimer -= Time.deltaTime;
            int getal = Mathf.CeilToInt(countdownTimer);
            countdownDisplay.text = getal > 0 ? getal.ToString() : "GO!";

            if (countdownTimer <= -0.5f)
            {
                countingDown = false;
                countdownDisplay.gameObject.SetActive(false);
                timerTextObject.SetActive(true);
                timeRemaining = gameDuration;
                gameRunning = true;
                goatGlider.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                goatGlider.StartGame();
            }
            return;
        }

        if (!gameRunning) return;

        // 2. Game over check
        Camera cam = Camera.main;
        Vector3 geitSchermPos = cam.WorldToViewportPoint(goatGlider.transform.position);
        if (geitSchermPos.y < -0.3f || geitSchermPos.y > 1.3f)
        {
            gameRunning = false;
            GameOver();
            return;
        }

        // 3. Timer
        timeRemaining -= Time.deltaTime;
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerDisplay.text = $"{seconds}s";

        if (timeRemaining <= 0f)
        {
            gameRunning = false;
            OnTimerFinished();
        }
    }

    void GameOver()
    {
        goatGlider.StopGame();
        backgroundScroller.StopScrolling();
        ground.StopScrolling();
        timerTextObject.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void OnReplay()
    {
        gameOverPanel.SetActive(false);
        goatGlider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        goatGlider.StopGame();
        countdownDisplay.gameObject.SetActive(true);
        countdownTimer = countdownDuur;
        countingDown = true;
        backgroundScroller.StartScrolling();
        ground.StartScrolling();
    }

    void OnTimerFinished()
    {
        goatGlider.StopGame();
        backgroundScroller.StopScrolling();
        ground.StopScrolling();
        timerTextObject.SetActive(false);
        finishButton.SetActive(true);
    }
}