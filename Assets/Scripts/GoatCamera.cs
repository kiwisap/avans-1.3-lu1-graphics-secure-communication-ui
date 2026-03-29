using UnityEngine;

public class GoatCamera : MonoBehaviour
{
    public Transform goat;
    public float offsetX = 0f;
    public float offsetY = 2f;

    void LateUpdate()
    {
        if (goat == null) return;

        transform.position = new Vector3(
            transform.position.x,
            goat.position.y + offsetY,
            transform.position.z
        );
    }
}