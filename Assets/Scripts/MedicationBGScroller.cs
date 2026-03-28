using UnityEngine;

public class MedicationBGScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float resetX = -1100f;
    public float startX = 1100f;

    private bool scrolling = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    public void StartScrolling() => scrolling = true;

    public void StopScrolling()
    {
        scrolling = false;
        transform.position = startPosition; // Reset naar beginpositie
    }

    void Update()
    {
        if (!scrolling) return;

        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x < resetX)
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }
}