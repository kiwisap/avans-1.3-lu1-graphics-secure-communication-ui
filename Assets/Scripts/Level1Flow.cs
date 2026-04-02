using UnityEngine;

public class Level1Flow : MonoBehaviour
{
    public GameObject[] steps;
    private int currentStep = 0;

    public void NextStep()
    {
        steps[currentStep].SetActive(false);

        currentStep++;

        if (currentStep < steps.Length)
        {
            steps[currentStep].SetActive(true);
        }
    }
}