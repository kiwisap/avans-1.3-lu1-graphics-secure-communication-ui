using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EdgeCollider2D))]
public class ProceduralGround : MonoBehaviour
{
    [Header("Grond vorm")]
    public int aantalPunten = 200;
    public float breedte = 300f;
    public float golfHoogte = 2f;
    public float golfFrequentie = 0.08f;
    public float basisHoogte = -2f;

    [Header("Scroll")]
    public GoatGlider goatGlider;
    private bool scrolling = false;
    private float offset = 0f;

    [Header("Volg geit")]
    public Transform goat;

    [Header("Visueel")]
    public LineRenderer lineRenderer;

    void Start()
    {
        // Object altijd op 0 houden
        transform.position = Vector3.zero;
        BerekenEnZetPunten();
    }

    public void StartScrolling() => scrolling = true;
    public void StopScrolling() => scrolling = false;

    float GetY(float x)
    {
        return basisHoogte
            + Mathf.Sin((x + offset) * golfFrequentie) * golfHoogte
            + Mathf.Sin((x + offset) * golfFrequentie * 1.7f) * (golfHoogte * 0.2f);
    }

    void BerekenEnZetPunten()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Vector2[] punten = new Vector2[aantalPunten];
        Vector3[] lijnPunten = new Vector3[aantalPunten];

        // Grond start vanaf geit X positie en loopt ver naar rechts
        float goatX = goat != null ? goat.position.x : 0f;
        float startX = goatX - breedte * 0.2f;  // Klein stukje achter de geit
        float stap = breedte / (aantalPunten - 1);

        for (int i = 0; i < aantalPunten; i++)
        {
            float x = startX + i * stap;
            float y = GetY(x);
            punten[i] = new Vector2(x, y);
            lijnPunten[i] = new Vector3(x, y, 0);
        }

        edge.SetPoints(new List<Vector2>(punten));

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = aantalPunten;
            lineRenderer.SetPositions(lijnPunten);
        }
    }

    void Update()
    {
        if (!scrolling || goatGlider == null) return;

        offset += goatGlider.HorizontaleSnelheid * Time.deltaTime;
        BerekenEnZetPunten();
    }
}