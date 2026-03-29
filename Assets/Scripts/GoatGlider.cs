using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;

public class GoatGlider : MonoBehaviour
{
    [Header("Referenties")]
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Gravity")]
    public float gravityLow = 0.8f;
    public float gravityHigh = 3.5f;
    public float gravitySmoothSpeed = 5f;

    [Header("Horizontale snelheid")]
    public float minSpeed = 2f;
    public float maxSpeed = 12f;
    public float versnellingFactor = 0.5f; // Hoe snel hij versnelt bij dalen

    [Header("Rotatie")]
    public float rotatieSmoothSpeed = 5f;
    public float maxRotatie = 45f;

    private bool gameActive = false;
    private float horizontaleSnelheid;

    // Publiek zodat de scroller dit kan uitlezen
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
        rb.linearVelocity = new Vector2(minSpeed, 0f); // Echte horizontale snelheid!
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

        // Horizontale snelheid gebaseerd op verticale beweging
        float vy = rb.linearVelocity.y;
        horizontaleSnelheid -= vy * versnellingFactor * Time.deltaTime;
        horizontaleSnelheid = Mathf.Clamp(horizontaleSnelheid, minSpeed, maxSpeed);

        // Zet echte horizontale snelheid op rigidbody
        rb.linearVelocity = new Vector2(horizontaleSnelheid, rb.linearVelocity.y);

        // Rotatie
        float targetRotatie = Mathf.Clamp(-vy * 4f, -maxRotatie, maxRotatie);
        float huidigeRotatie = transform.eulerAngles.z > 180
            ? transform.eulerAngles.z - 360
            : transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0, 0,
            Mathf.Lerp(huidigeRotatie, targetRotatie, rotatieSmoothSpeed * Time.deltaTime));
    }
}