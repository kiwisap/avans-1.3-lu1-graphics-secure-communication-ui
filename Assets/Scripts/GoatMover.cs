using UnityEngine;
using System.Collections;
using System;

public class GoatMover : MonoBehaviour
{
    public Transform[] levelPoints;
    public float moveSpeed = 500f;

    private RectTransform rectTransform;

    private void OnEnable()
    {
        AccountManager.OnLoginSuccess += HandleLoginSuccess;
    }

    private void OnDisable()
    {
        AccountManager.OnLoginSuccess -= HandleLoginSuccess;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        int goatIndex = Mathf.Max(0, currentLevel - 1);
        rectTransform.anchoredPosition =
            levelPoints[goatIndex].GetComponent<RectTransform>().anchoredPosition;
    }

    public void MoveToLevel(int levelIndex)
    {
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

    private void HandleLoginSuccess()
    {
        var currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        MoveToLevel(currentLevel);
    }
}