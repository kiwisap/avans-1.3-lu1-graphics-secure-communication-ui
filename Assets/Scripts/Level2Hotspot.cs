using UnityEngine;
using TMPro;
using System.Collections;

public class Level2Hotspots : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public GameObject finishButton;

    private bool mouthpieceDone = false;
    private bool screenDone = false;
    private bool airDone = false;

    public void ShowMouthpiece()
    {
        infoText.text = "Hier blaas je in. Sluit je lippen goed aan.";
        mouthpieceDone = true;
        CheckCompletion();
    }

    public void ShowScreen()
    {
        infoText.text = "Hier ziet de dokter hoe hard je blaast.";
        screenDone = true;
        CheckCompletion();
    }

    public void ShowAir()
    {
        infoText.text = "De lucht gaat door het apparaat heen";
        airDone = true;
        CheckCompletion();
    }

    void CheckCompletion()
    {
        if (mouthpieceDone && screenDone && airDone)
        {
            StartCoroutine(ShowFinish());
        }
    }

    IEnumerator ShowFinish()
    {
        yield return new WaitForSeconds(1.5f);

        finishButton.SetActive(true);
        infoText.text = "Goed gedaan! Je hebt alles ontdekt";
    }
}
