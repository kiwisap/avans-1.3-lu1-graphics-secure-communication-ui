using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public RectTransform bg1;
    public RectTransform bg2A;
    public RectTransform bg2B;

    public float scrollSpeed = 0f;

    private float bg1Height;
    private float bg2Height;

    private bool introFinished = false;

    void Start()
    {
        bg1Height = bg1.rect.height;
        bg2Height = bg2A.rect.height;
    }

    void Update()
    {
        if (scrollSpeed <= 0f) return;

        if (!introFinished)
        {
            bg1.anchoredPosition += Vector2.down * scrollSpeed * Time.deltaTime;

            if (bg1.anchoredPosition.y <= -bg1Height)
            {
                introFinished = true;
                bg1.gameObject.SetActive(false);
            }
        }

        bg2A.anchoredPosition += Vector2.down * scrollSpeed * Time.deltaTime;
        bg2B.anchoredPosition += Vector2.down * scrollSpeed * Time.deltaTime;

        if (bg2A.anchoredPosition.y <= -bg2Height)
        {
            bg2A.anchoredPosition = new Vector2(0, bg2B.anchoredPosition.y + bg2Height);
        }

        if (bg2B.anchoredPosition.y <= -bg2Height)
        {
            bg2B.anchoredPosition = new Vector2(0, bg2A.anchoredPosition.y + bg2Height);
        }
    }
}