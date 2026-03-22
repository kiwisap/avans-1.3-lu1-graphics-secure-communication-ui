using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CircleTransition : MonoBehaviour
{
    private RectTransform circle;
    public float speed = 3000f;

    void Start()
    {
        circle = GetComponent<RectTransform>();
    }

    public void StartTransition(string sceneName)
    {
        StartCoroutine(Close(sceneName));
    }

    IEnumerator Close(string sceneName)
    {
        while (circle.sizeDelta.x < 3000)
        {
            circle.sizeDelta += Vector2.one * speed * Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}