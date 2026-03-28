using UnityEngine;

public class GoatGlider : MonoBehaviour
{
    public Rigidbody2D rb;
    public float gravityLow = 1f;
    public float gravityHigh = 4f;
    public float maxFallSpeed = 10f;

    private bool gameActive = false;

    public void StartGame()
    {
        gameActive = true;
        rb.simulated = true;
        gameObject.SetActive(true);
    }

    public void StopGame()
    {
        gameActive = false;
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
    }

    void Start()
    {
        rb.simulated = false;
        gameObject.SetActive(false); // Geit verborgen bij start
    }

    void Update()
    {
        if (!gameActive) return;

        if (Input.GetMouseButton(0))
            rb.gravityScale = gravityHigh;
        else
            rb.gravityScale = gravityLow;

        if (rb.linearVelocity.y < -maxFallSpeed)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
    }
}