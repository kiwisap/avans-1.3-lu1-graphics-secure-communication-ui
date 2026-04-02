using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public GoatMover goatMover;
    public CircleTransition transition;

    public string sceneName;

    public void ClickLevel()
    {
        StartCoroutine(LevelFlow());
    }

    IEnumerator LevelFlow()
    {
        goatMover.MoveToLevel(levelIndex);

        yield return new WaitForSeconds(1.2f);

        transition.StartTransition(sceneName);
    }
}