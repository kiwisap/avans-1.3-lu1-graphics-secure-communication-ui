using UnityEngine;

public class MedicationBGScroller : MonoBehaviour
{
    public RectTransform bgA;
    public RectTransform bgB;
    public float scrollSpeed = 300f;
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
        if (!scrolling) return;

        bgA.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;
        bgB.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // Volledig van het scherm af = positie onder -960 (want start is 960, breedte 1920)
        if (bgA.anchoredPosition.x < -960f)
            bgA.anchoredPosition = new Vector2(bgB.anchoredPosition.x + bgWidth, startY);

        if (bgB.anchoredPosition.x < -960f)
            bgB.anchoredPosition = new Vector2(bgA.anchoredPosition.x + bgWidth, startY);
    }
}