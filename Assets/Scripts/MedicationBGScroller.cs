using UnityEngine;

public class MedicationBGScroller : MonoBehaviour
{
    public RectTransform bgA;
    public RectTransform bgB;
    public GoatGlider goatGlider; // Koppel de Goat hieraan
    public float scrollSpeedMultiplier = 80f; // Canvas units per game unit
    private bool scrolling = false;
    private float bgWidth = 1920f;
    private float startY = 540f;

    void Start()
    {
        scrolling = false;
        bgA.anchoredPosition = new Vector2(960, startY);
        bgB.anchoredPosition = new Vector2(960 + bgWidth, startY);
    }

    public void StartScrolling() => scrolling = true;
    public void StopScrolling() => scrolling = false;

    void Update()
    {
        if (!scrolling || goatGlider == null) return;

        float speed = goatGlider.HorizontaleSnelheid * scrollSpeedMultiplier * Time.deltaTime;

        bgA.anchoredPosition += Vector2.left * speed;
        bgB.anchoredPosition += Vector2.left * speed;

        if (bgA.anchoredPosition.x < -960f)
            bgA.anchoredPosition = new Vector2(bgB.anchoredPosition.x + bgWidth, startY);

        if (bgB.anchoredPosition.x < -960f)
            bgB.anchoredPosition = new Vector2(bgA.anchoredPosition.x + bgWidth, startY);
    }
}