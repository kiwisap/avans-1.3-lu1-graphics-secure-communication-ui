using UnityEngine;
using System.Collections;

public class GoatMover : MonoBehaviour
{
    public Transform[] levelPoints;
    public float moveSpeed = 500f;

    private RectTransform rectTransform;
    private int currentLevel = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void MoveToLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        StopAllCoroutines();
        StartCoroutine(MoveGoat(levelPoints[levelIndex].GetComponent<RectTransform>().anchoredPosition));
    }

    IEnumerator MoveGoat(Vector2 target)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, target) > 1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}