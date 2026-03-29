using UnityEngine;

public class GoatCamera : MonoBehaviour
{
    public Transform goat;
    public float offsetX = -3f;
    public float offsetY = 2f;

    void LateUpdate()
    {
        if (goat == null) return;

        transform.position = new Vector3(
            goat.position.x + offsetX,
            goat.position.y + offsetY,
            transform.position.z
        );
    }
}