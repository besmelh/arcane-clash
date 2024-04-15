using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEVisualizerController : MonoBehaviour
{
    public float radius = 5f;
    public int segments = 360;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawAOEVisualizer();
    }

    private void DrawAOEVisualizer()
    {
        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i <= segments; i++)
        {
            float angle = ((float)i / (float)segments) * Mathf.PI * 2f;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0f, z));
        }
    }

    public void SetRadius(float radius)
    {
        this.radius = radius;
    }
}
