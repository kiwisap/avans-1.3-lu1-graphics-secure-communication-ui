using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class GoatGlider : MonoBehaviour
{
    [Header("Referenties")]
    public Rigidbody2D rb;

    [Header("Besturing")]
    public float gravityLow = 0.8f;
    public float gravityHigh = 3.5f;
    public float maxFallSpeed = 12f;
    public float maxRiseSpeed = 8f;
    public float gravitySmoothSpeed = 5f;

    private bool gameActive = false;

    void Awake()
    {
        EnhancedTouchSupport.Enable();
        rb.simulated = false;
    }

    public void StartGame()
    {
        Debug.Log("StartGame aangeroepen!");
        gameActive = true;
        rb.simulated = true;
    }

    public void StopGame()
    {
        gameActive = false;
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (!gameActive) return;

        bool pressing = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0;

        rb.gravityScale = Mathf.Lerp(rb.gravityScale, pressing ? gravityHigh : gravityLow, gravitySmoothSpeed * Time.deltaTime);

        float vy = Mathf.Clamp(rb.linearVelocity.y, -maxFallSpeed, maxRiseSpeed);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, vy);
    }
}