using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("UI")]
    public Image fadePanel;
    public float fadeDuur = 1f;

    void Start()
    {
        // Fade in bij opstarten
        StartCoroutine(FadeIn());
    }

    public void OnKindGekozen()
    {
        PlayerPrefs.SetString("GebruikerType", "kind");
        PlayerPrefs.Save();
        StartCoroutine(FadeNaarScene("WorldOverview"));
    }

    public void OnOuderGekozen()
    {
        PlayerPrefs.SetString("GebruikerType", "ouder");
        PlayerPrefs.Save();
        StartCoroutine(FadeNaarScene("WorldOverview"));
    }

    IEnumerator FadeIn()
    {
        // Start zwart, fade naar transparant
        Color kleur = fadePanel.color;
        kleur.a = 1f;
        fadePanel.color = kleur;

        float timer = 0f;
        while (timer < fadeDuur)
        {
            timer += Time.deltaTime;
            kleur.a = 1f - (timer / fadeDuur);
            fadePanel.color = kleur;
            yield return null;
        }

        kleur.a = 0f;
        fadePanel.color = kleur;
        fadePanel.raycastTarget = false;
    }

    IEnumerator FadeNaarScene(string sceneName)
    {
        fadePanel.raycastTarget = true;
        Color kleur = fadePanel.color;
        kleur.a = 0f;
        fadePanel.color = kleur;

        float timer = 0f;
        while (timer < fadeDuur)
        {
            timer += Time.deltaTime;
            kleur.a = timer / fadeDuur;
            fadePanel.color = kleur;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}