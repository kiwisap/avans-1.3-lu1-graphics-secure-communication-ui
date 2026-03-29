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
    public MedicationBGScroller backgroundScroller; // Nu één scroller
    public float gameDuration = 60f;

    private float timeRemaining;
    private bool gameRunning = false;

    public ProceduralGround ground;

    void Start()
    {
        startPanel.SetActive(true);
        timerTextObject.SetActive(false);
        finishButton.SetActive(false);
        goatGlider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        goatGlider.StopGame();
    }

    public void OnMedicationConfirmed()
    {
        Debug.Log("Countdown active: " + countdownDisplay.gameObject.activeSelf);
        Debug.Log("Countdown text: " + countdownDisplay.text);
        Debug.Log("Medication confirmed aangeroepen!");
        Debug.Log("GoatGlider object: " + goatGlider);
        startPanel.SetActive(false);
        goatGlider.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        countdownDisplay.gameObject.SetActive(true);
        countdownTimer = countdownDuur;
        countingDown = true;
        backgroundScroller.StartScrolling();
        ground.StartScrolling();
    }

    void Update()
    {
        // Countdown loopt ALTIJD, niet alleen als gameRunning
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
                goatGlider.StartGame();
            }
            return;
        }

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

        backgroundScroller.StopScrolling();

        ground.StopScrolling();

        timerTextObject.SetActive(false);
        finishButton.SetActive(true);
    }
}