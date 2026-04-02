using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldOverviewManager : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;

    private Button[] levelButtons;

    [Header("Karakters")]
    public GoatMover goatMover;
    public RectTransform herder; // UI element in Canvas

    [Header("Speech Bubble")]
    public GameObject speechBubble;
    public TextMeshProUGUI speechBubbleTekst;

    [Header("Level Punten voor Herder")]
    public Transform[] herderLevelPoints;

    private string[] levelUitleg = {
        "Level 1: Je kind maakt kennis met het longonderzoek. Ze leren dat het geen wedstrijd is en worden tot rust gesteld",
        "Level 2: Je kind leert over de spirometer door de klikken op verschillende onderdelen.",
        "Level 3: Je kind doet een echte meting in de vorm van een leuk spelletje waar ze de lucht in gaan met een ballon.",
        "Level 4: Hier wordt een spirometer gesimuleerd en hoe die in zijn werking gaat met het trainen en testen",
        "Level 5: Je kind neemt zijn medicijnen en heeft even de tijd om een spelletje te spelen om tot rust te komen"
    };

    void Start()
    {
        levelButtons = new[] {
            level1Button,
            level2Button,
            level3Button,
            level4Button,
            level5Button
        };

        // Voortgang tonen
        UpdateLevelUI();

        speechBubble.SetActive(false);

        if (GameState.IsOuder)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = true;
            level4Button.interactable = true;
            level5Button.interactable = true;

            level1Button.image.color = Color.green;
            level2Button.image.color = Color.green;
            level3Button.image.color = Color.green;
            level4Button.image.color = Color.green;
            level5Button.image.color = Color.green;

            herder.gameObject.SetActive(true);

            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            int herderIndex = Mathf.Max(0, currentLevel - 1);
            herder.anchoredPosition = herderLevelPoints[herderIndex]
                .GetComponent<RectTransform>().anchoredPosition;

            level1Button.onClick = new Button.ButtonClickedEvent();
            level2Button.onClick = new Button.ButtonClickedEvent();
            level3Button.onClick = new Button.ButtonClickedEvent();
            level4Button.onClick = new Button.ButtonClickedEvent();
            level5Button.onClick = new Button.ButtonClickedEvent();

            level1Button.onClick.AddListener(() => ToonUitlegEnBeweegHerder(0));
            level2Button.onClick.AddListener(() => ToonUitlegEnBeweegHerder(1));
            level3Button.onClick.AddListener(() => ToonUitlegEnBeweegHerder(2));
            level4Button.onClick.AddListener(() => ToonUitlegEnBeweegHerder(3));
            level5Button.onClick.AddListener(() => ToonUitlegEnBeweegHerder(4));
        }
        else
        {
            herder.gameObject.SetActive(false);
        }
    }

    public void ToonUitlegEnBeweegHerder(int levelIndex)
    {
        // Herder beweegt naar het level
        StartCoroutine(BeweegHerder(
            herderLevelPoints[levelIndex].GetComponent<RectTransform>().anchoredPosition));

        // Speech bubble tonen
        speechBubble.SetActive(true);
        speechBubbleTekst.text = levelUitleg[levelIndex];
    }

    System.Collections.IEnumerator BeweegHerder(Vector2 target)
    {
        while (Vector2.Distance(herder.anchoredPosition, target) > 1f)
        {
            herder.anchoredPosition = Vector2.MoveTowards(
                herder.anchoredPosition,
                target,
                500f * Time.deltaTime
            );
            yield return null;
        }
    }

    public void SluitSpeechBubble()
    {
        speechBubble.SetActive(false);
    }

    private void UpdateLevelUI()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < currentLevel)
            {
                levelButtons[i].image.color = Color.green;
            }
        }
    }
}