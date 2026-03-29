using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;

public class GoatGlider : MonoBehaviour
{
    [Header("Referenties")]
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Gravity")]
    public float gravityLow = 0.05f;
    public float gravityHigh = 2.0f;
    public float gravitySmoothSpeed = 8f;

    [Header("Horizontale snelheid")]
    public float minSpeed = 3f;
    public float maxSpeed = 6f;
    public float versnellingFactor = 0.3f;

    [Header("Rotatie")]
    public float rotatieSmoothSpeed = 8f;
    public float maxRotatie = 35f;

    private bool gameActive = false;
    private float horizontaleSnelheid;

    public float HorizontaleSnelheid => horizontaleSnelheid;

    void Awake()
    {
        EnhancedTouchSupport.Enable();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        rb.simulated = false;
        horizontaleSnelheid = minSpeed;
    }

    public void StartGame()
    {
        gameActive = true;
        rb.simulated = true;
        spriteRenderer.enabled = true;
        horizontaleSnelheid = minSpeed;
        rb.linearVelocity = new Vector2(minSpeed, 0f);
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

        bool pressing = Keyboard.current != null && Keyboard.current.spaceKey.isPressed
            || Mouse.current != null && Mouse.current.leftButton.isPressed
            || UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0;

        float targetGravity = pressing ? gravityHigh : gravityLow;
        rb.gravityScale = Mathf.Lerp(rb.gravityScale, targetGravity, gravitySmoothSpeed * Time.deltaTime);

        float vy = rb.linearVelocity.y;
        horizontaleSnelheid -= vy * versnellingFactor * Time.deltaTime;
        horizontaleSnelheid = Mathf.Clamp(horizontaleSnelheid, minSpeed, maxSpeed);

        // Verticale snelheid begrenzen zodat hij niet buiten beeld vliegt
        float clampedVy = Mathf.Clamp(vy, -10f, 5f);
        rb.linearVelocity = new Vector2(horizontaleSnelheid, clampedVy);

        float targetRotatie = Mathf.Clamp(-vy * 4f, -maxRotatie, maxRotatie);
        float huidigeRotatie = transform.eulerAngles.z > 180
            ? transform.eulerAngles.z - 360
            : transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Lerp(huidigeRotatie, targetRotatie, rotatieSmoothSpeed * Time.deltaTime));
    }
}