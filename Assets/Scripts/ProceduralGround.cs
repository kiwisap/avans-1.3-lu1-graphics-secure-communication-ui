using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EdgeCollider2D))]
public class ProceduralGround : MonoBehaviour
{
    [Header("Grond vorm")]
    public int aantalPunten = 80;
    public float breedte = 80f;
    public float golfHoogte = 2f;
    public float golfFrequentie = 0.3f;
    public float basisHoogte = -2.5f;

    [Header("Scroll")]
    public float scrollSpeed = 2.5f;
    private bool scrolling = false;

    [Header("Visueel")]
    public LineRenderer lineRenderer;

    private Vector2[] punten;
    private float offset = 0f;

    public void StartScrolling() => scrolling = true;
    public void StopScrolling() => scrolling = false;

    void Start()
    {
        GenereerGrond();
    }

    void GenereerGrond()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        punten = new Vector2[aantalPunten];

        BerekenPunten();
        edge.SetPoints(new List<Vector2>(punten));

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = aantalPunten;
            UpdateLijnRenderer();
        }
    }

    void BerekenPunten()
    {
        for (int i = 0; i < aantalPunten; i++)
        {
            float x = (i / (float)(aantalPunten - 1)) * breedte - breedte / 2f;
            float xSample = x + offset;
            float y = basisHoogte
                + Mathf.Sin(xSample * golfFrequentie) * golfHoogte
                + Mathf.Sin(xSample * golfFrequentie * 2.7f) * (golfHoogte * 0.3f);
            punten[i] = new Vector2(x, y);
        }
    }

    void UpdateLijnRenderer()
    {
        Vector3[] lijnPunten = new Vector3[aantalPunten];
        for (int i = 0; i < aantalPunten; i++)
            lijnPunten[i] = new Vector3(punten[i].x, punten[i].y, 0);
        lineRenderer.SetPositions(lijnPunten);
    }

    void Update()
    {
        if (!scrolling) return;

        offset += scrollSpeed * Time.deltaTime;

        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        BerekenPunten();
        edge.SetPoints(new List<Vector2>(punten));

        if (lineRenderer != null)
            UpdateLijnRenderer();
    }
}